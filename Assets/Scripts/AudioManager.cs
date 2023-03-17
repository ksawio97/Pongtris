using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private Sound[] musicSounds, sfxSounds;

    [SerializeField]
    private AudioSource musicSource, sfxSource;

    public void Awake()
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

    public void PlayMusic(string name)
    {
        var sound = Array.Find(musicSounds, x => x.name == name);
        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySound(string name)
    {
        var sound = Array.Find(sfxSounds, x => x.name == name);
        if (sound != null)
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }

    public void ChangeVolumesOfType(SoundType type, float volume)
    {
        if (type == SoundType.Music)
            musicSource.volume = volume;
        else 
            sfxSource.volume = volume;
    }
}
