﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerCtrl : MonoBehaviour
{   //use this script for volume control (not use yet)
    public AudioMixer audioMixer;
    [Space(10)]//creat sapce between list in inspecter make it easyer to look
    public Slider musicSlider;
    public Slider sfxSlider;


    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);//change  0.001 to 1 of slider value to -80 and 0 of mixer 
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);//change 0.001 to 1 of slider value to -80 and 0 of mixer 
    }

    void Start()
    {   //use this when going to make volume slider
        //musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0);
        //sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0);
    }
    
    private void OnDisable()//only save when exit scene
    {
        float musicVolume = 0;
        float sfxVolume = 0;

        audioMixer.GetFloat("musicVolume", out musicVolume);
        audioMixer.GetFloat("sfxVolume", out sfxVolume);

        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume",  sfxVolume);
        PlayerPrefs.Save();
    }


}
