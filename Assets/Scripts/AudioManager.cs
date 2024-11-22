using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public List<AudioClip> musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;
    void Awake()
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

    
    void Start()
    {
        PlayMusic("MainTheme");
    }

    // Finds AudioClip from musicSounds by name
    private AudioClip findMusicClipByName(string clipName)
    {
        foreach (AudioClip clip in musicSounds)
        {
            if (clip.name == clipName)
            {
                return clip; 
            }
        }
        return null; 
    }

    // Finds AudioClip from sfxSounds by name
    private AudioClip findSFXClipByName(string clipName)
    {
        foreach (AudioClip clip in sfxSounds)
        {
            if (clip.name == clipName)
            {
                return clip; 
            }
        }
        return null; 
    }

    public void PlayMusic(string name)
    {
         musicSource.clip = findMusicClipByName(name);
         musicSource.Play();   
    }
    
    public void PlaySFX(string name)
    {
        sfxSource.pitch = Random.Range(pitchMin,pitchMax);
        sfxSource.PlayOneShot(findSFXClipByName(name));
        
    }
}
