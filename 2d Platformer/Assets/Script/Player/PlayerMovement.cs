﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//http://dotween.demigiant.com/getstarted.php

public class PlayerMovement : MonoBehaviour
{   //put in player game obj
    private CollisionCheck collisionCheck;
    [HideInInspector]
    public Rigidbody2D rb2d;
    private AnimationScript anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Effect")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    // Start is called before the first frame update
    void Start()
    {
        collisionCheck = GetComponent<CollisionCheck>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        anim.SetHorizontalMovement(x, y, rb2d.velocity.y);

        if (collisionCheck.onWall && Input.GetButton("Fire3") && canMove)        
        { //press fire3(Shift) to grab on wall and stop sliding down
            if (side != collisionCheck.wallSide)
            {
                anim.Flip(side * -1);
            }
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !collisionCheck.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (collisionCheck.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<Jumping>().enabled = true;
        }

        if (wallGrab && !isDashing)
        {
            rb2d.gravityScale = 0;
            if (x > .2f || x < -.2f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            }

            float speedModifier = y > 0 ? .5f : 1;

            rb2d.velocity = new Vector2(rb2d.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb2d.gravityScale = 3;
        }

        if (collisionCheck.onWall && !collisionCheck.onGround)      
        { // when on wall and not on the ground slide down
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!collisionCheck.onWall || collisionCheck.onGround)
        {
            wallSlide = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");

            if (collisionCheck.onGround)
            {
                Jump(Vector2.up, false);
            }
            if (collisionCheck.onWall && !collisionCheck.onGround)
            {
                WallJump();
            }              
        }

        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0)
            {
                Dash(xRaw, yRaw);
            }
        }

        if (collisionCheck.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!collisionCheck.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
        {
            return;
        }

        if (x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        side = anim.spriteRenderer.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    private void Dash(float x, float y)   
    {     //get direcrion from horizontal and verticle axis and add it velocity to rigibody
        hasDashed = true;

        anim.SetTrigger("dash");

        rb2d.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb2d.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        FindObjectOfType<DashEffect>().ShowGhost();//creat follow ghost effect
        StartCoroutine(GroundDash());//start count time to dash again
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        rb2d.gravityScale = 0;
        GetComponent<Jumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb2d.gravityScale = 3;
        GetComponent<Jumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (collisionCheck.onGround)
        {
            hasDashed = false;
        }
    }

    private void WallJump()
    {
        if ((side == 1 && collisionCheck.onRightWall) || side == -1 && !collisionCheck.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }
        //pervent player for repetly use wall jump
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = collisionCheck.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void WallSlide()
    {
        if (collisionCheck.wallSide != side)
        {
            anim.Flip(side * -1);
        }

        if (!canMove)
        {
            return;
        }

        bool pushingWall = false;
        if ((rb2d.velocity.x > 0 && collisionCheck.onRightWall) 
            || (rb2d.velocity.x < 0 && collisionCheck.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb2d.velocity.x;

        rb2d.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
        //modify velocity of rigibody by getting value of horizontal and vertical axis
    {
        if (!canMove)
        {
            return;
        }

        if (wallGrab)
        {
            return;
        }

        if (!wallJumped)
        {
            rb2d.velocity = new Vector2(dir.x * speed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, (new Vector2(dir.x * speed, rb2d.velocity.y)), wallJumpLerp * Time.deltaTime);  
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.velocity += dir * jumpForce;

        particle.Play();
    }

    void RigidbodyDrag(float x)
    {
        rb2d.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = collisionCheck.onRightWall ? 1 : -1;
        return particleSide;
    }
}
