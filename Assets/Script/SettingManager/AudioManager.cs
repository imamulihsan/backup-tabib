using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    // [SerializeField] Image sfxOn;
    // [SerializeField] Image sfxOff;
    // [SerializeField] Image musicOn;
    // [SerializeField] Image musicOff;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
            // UpdateButtonMusic();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
            // UpdateButtonSFX();
        }
    }

    // public void ToogleMusic()
    // {
    //     musicSource.mute = !musicSource.mute;
    //     // UpdateButtonMusic();
    // }

    // public void ToogleSFX()
    // {
    //     sfxSource.mute = !sfxSource.mute;
    //     // UpdateButtonSFX();
    // }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // private void UpdateButtonMusic()
    // {
    //     if (musicSource.mute == false)
    //     {
    //         musicOn.enabled = true;
    //         musicOff.enabled = false;
    //     }

    //     else
    //     {
    //         musicOn.enabled = false;
    //         musicOff.enabled = true;
    //     }
    // }

    // private void UpdateButtonSFX()
    // {
    //     if (sfxSource.mute == false)
    //     {
    //         sfxOn.enabled = true;
    //         sfxOff.enabled = false;
    //     }

    //     else
    //     {
    //         sfxOn.enabled = false;
    //         sfxOff.enabled = true;
    //     }
    // }
}
