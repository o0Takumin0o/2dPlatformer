using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFx : MonoBehaviour
{
    [Header("UI")]
    public AudioSource GameSfx;
    public AudioClip HoverFx;
    public AudioClip ClickFx;
    public AudioClip PickupSound;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void HoverSound()
    {
        GameSfx.PlayOneShot(HoverFx);
    }


    public void ClickSound()
    {
        GameSfx.PlayOneShot(ClickFx);
    }
    

    public void CheckSound()
    {
        GameSfx.PlayOneShot(HoverFx);
    }

    public void Pickup()
    {
        GameSfx.PlayOneShot(PickupSound);
    }
    
}
