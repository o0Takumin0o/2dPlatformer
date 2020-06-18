using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DashEffect : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AnimationScript anim;
    private SpriteRenderer sr;
    public Transform DashParent;
    public Color trailColor;
    public Color fadeColor;
    public float DashInterval;
    public float fadeTime;

    private void Start()
    {
        anim = FindObjectOfType<AnimationScript>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void ShowGhost()
    {   // create sequencer using DoTween and handle multiple of sequencer
        Sequence s = DOTween.Sequence();
        
        for (int i = 0; i < DashParent.childCount; i++)
        {
            Transform currentGhost = DashParent.GetChild(i);
            s.AppendCallback(() => currentGhost.position =
            playerMovement.transform.position);

            s.AppendCallback(() => currentGhost.GetComponent
                <SpriteRenderer>().flipX = anim.spriteRenderer.flipX);

            s.AppendCallback(() => currentGhost.GetComponent
                <SpriteRenderer>().sprite = anim.spriteRenderer.sprite);

            s.Append(currentGhost.GetComponent
                <SpriteRenderer>().material.DOColor(trailColor, 0));

            s.AppendCallback(() => FadeSprite(currentGhost));
            s.AppendInterval(DashInterval);
        }
    }

    public void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }
}
