using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsButtonEvent : MonoBehaviour
{
    private static string sound = "UIClick";

    [SerializeField]
    Button goBack;
    void Start()
    {
        Action<string> playSound = AudioManager.Instance.PlaySound;
        
        goBack.onClick.AddListener(() => { playSound(sound); SceneManager.LoadScene(0); });
    }
}
