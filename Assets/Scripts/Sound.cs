using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public enum SoundType
{
    Music,
    Sfx
}
