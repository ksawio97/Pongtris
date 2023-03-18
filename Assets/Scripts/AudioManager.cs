using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private Sound[] musicSounds, sfxSounds, uiSounds;

    [SerializeField]
    private AudioSource musicSource, sfxSource, uiSource;

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
    private void Start()
    {
        LoadSettings();
        PlayMusic("Menu Track");
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void OnSceneChanged(Scene sc1, Scene sc2)
    {
        //Game music
        if (sc2.buildIndex == 1)
            PlayMusic("Game Track");
        //lobby music
        else
            PlayMusic("Menu Track");
    }
    void LoadSettings()
    {
        var settingsData = GameSaveLoad.Load<SettingsData>(GameSaveLoad.settingsPath);
        if(settingsData == null)
            return;
        
        foreach(var settingData in settingsData.settingsData)
        {
            ChangeVolumesOfType(settingData.type, settingData.volume);
        }
    }

    public void PlayMusic(string name)
    {
        var sound = Array.Find(musicSounds, x => x.name == name);
        if (sound != null && musicSource.clip != sound.clip)
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

    public void PlayUISound(string name)
    {
        var sound = Array.Find(uiSounds, x => x.name == name);
        if (sound != null)
        {
            uiSource.PlayOneShot(sound.clip);
        }
    }

    public void ChangeVolumesOfType(SoundType type, float volume)
    {
        if (type == SoundType.Music)
            musicSource.volume = volume;
        else if(type == SoundType.Sfx)
            sfxSource.volume = volume;
        else
            uiSource.volume = volume;
    }
}
