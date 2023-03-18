using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSectionHandle : MonoBehaviour
{
    [SerializeField]
    public SoundType type;

    private Slider slider;

    public float sliderValue
    {
        get
        {
            return slider.value;
        }
    }

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void LoadSliderValue(float volume)
    {
        slider.value = volume;
    }
}