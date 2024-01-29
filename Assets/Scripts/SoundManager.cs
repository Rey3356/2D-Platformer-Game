using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource gameSFX;
    public AudioSource gameMusic;

    public SoundType[] Sounds;

    public bool isMute;

    [Range(0.0f, 1.0f)]
    public float volume = 1f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic(global::Sounds.Music);
    }

    public void Mute(bool status)
    {
        isMute = status;
        gameMusic.mute = status;
        gameSFX.mute = status;
    }
    
    public void setVolume(float vol)
    {
        volume = vol;
        gameMusic.volume = volume;
        gameSFX.volume = volume;
    }
    public void PlayMusic(Sounds sound)
    {
        if (!isMute)
        {
            AudioClip clip = getSoundClip(sound);
            if (clip != null)
            {
                gameMusic.clip = clip;
                gameMusic.Play();
            }
            else
            {
                Debug.LogError("Clip not found for sound type : " + sound);
            }
        }
        
    }

    public void PlaySFX(Sounds sound)
    {
        AudioClip clip = getSoundClip(sound);
        if(clip != null)
        {
            gameSFX.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip not found for sound type : " + sound);
        }
    }

    private AudioClip getSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if(item != null)
        {
            return item.soundClip;
        }
            return null;
    }
}

[Serializable]  
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}
public enum Sounds
{
    ButtonClick,
    ButtonClickWrong,
    Music,
    playerJump,
    playerDeath,
    playerHurt,
    playerWin,
    KeyCollect,
}
