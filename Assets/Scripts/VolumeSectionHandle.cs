using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSectionHandle : MonoBehaviour
{
    [SerializeField]
    SoundType type;

    Slider slider;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }
    //TO DO Add sounds to slider
    //TO DO Add saving to SaveData settings
    void OnDestroy()
    {
        AudioManager.Instance.ChangeVolumesOfType(type, slider.value);
    }
}