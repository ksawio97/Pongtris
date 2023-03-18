using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtonsEvents : MonoBehaviour
{
    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button menuButton;

    [SerializeField]
    private Button exitButton;

    private static string sound = "UIClick";
    void Start()
    {
        Action<string> playSound = AudioManager.Instance.PlayUISound;

        resumeButton.onClick.AddListener(() => { playSound(sound); SceneManager.LoadScene(1); });
        menuButton.onClick.AddListener(() => { playSound(sound);  SceneManager.LoadScene(0); });
        exitButton.onClick.AddListener(() => { playSound(sound); Application.Quit(); });
    }
}
