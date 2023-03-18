using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{
    private static string sound = "UIClick";

    [SerializeField]
    Button goBack;

    void Start()
    {
        Action<string> playSound = AudioManager.Instance.PlayUISound;
        
        goBack.onClick.AddListener(() => { SaveSettings(); playSound(sound); SceneManager.LoadScene(0); });
        LoadSettings();
    }

    void LoadSettings()
    {
        var settingsData = GameSaveLoad.Load<SettingsData>(GameSaveLoad.settingsPath);

        if (settingsData == null)
            return;

        var volumeSectionHandles = GetComponentsInChildren<VolumeSectionHandle>();
        foreach (var settingData in settingsData.settingsData)
        {
            var volumeSectionHandle = Array.Find(volumeSectionHandles, x => x.type == settingData.type);
            if (volumeSectionHandle != null)
                volumeSectionHandle.LoadSliderValue(settingData.volume);

        }
    }

    void SaveSettings()
    {
        var volumeSectionHandlers = GetComponentsInChildren<VolumeSectionHandle>();
        GameSaveLoad.SaveSettings(volumeSectionHandlers);
    }
}
