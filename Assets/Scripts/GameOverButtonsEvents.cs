using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameOverButtonsEvents : MonoBehaviour
{
    [SerializeField]
    private Button playAgainButton;

    [SerializeField]
    private Button menuButton;

    private static string sound = "UIClick";

    void Start()
    {
        Action<string> playSound = AudioManager.Instance.PlayUISound;

        AudioManager.Instance.PlaySound("Explosion");
        playAgainButton.onClick.AddListener(() => { playSound(sound); SceneManager.LoadScene(1); });
        menuButton.onClick.AddListener(() => { playSound(sound); SceneManager.LoadScene(0); });
    }
}
