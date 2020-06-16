using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    //put in player sprite
    private Animator anim;
    private PlayerMovement playerMovement;
    private CollisionCheck coll;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<CollisionCheck>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("onWall", coll.onWall);
        anim.SetBool("onRightWall", coll.onRightWall);
        anim.SetBool("wallGrab", playerMovement.wallGrab);
        anim.SetBool("wallSlide", playerMovement.wallSlide);
        anim.SetBool("canMove", playerMovement.canMove);
        anim.SetBool("isDashing", playerMovement.isDashing);
    }

    public void SetHorizontalMovement(float x,float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);

    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {   //flip character

        if (playerMovement.wallGrab || playerMovement.wallSlide)
        {
            if (side == -1 && spriteRenderer.flipX)
            {
                return;
            }

            if (side == 1 && !spriteRenderer.flipX)
            {
                return;
            }
        }

        bool state = (side == 1) ? false : true;
        spriteRenderer.flipX = state;
    }
}
