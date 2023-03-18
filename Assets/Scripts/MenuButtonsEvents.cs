using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonsEvents : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button exitButton;

    private static string sound = "UIClick";
    void Start()
    {
        Action<string> playSound = AudioManager.Instance.PlayUISound;

        playButton.onClick.AddListener(() => { playSound(sound);  SceneManager.LoadScene(1); });
        settingsButton.onClick.AddListener(() => { playSound(sound); SceneManager.LoadScene(2); });
        exitButton.onClick.AddListener(() => { playSound(sound); Application.Quit(); });
    }
}
