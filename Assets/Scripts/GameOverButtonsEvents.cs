using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverButtonsEvents : MonoBehaviour
{
    [SerializeField]
    private Button playAgainButton;

    [SerializeField]
    private Button menuButton;

    void Start()
    {
        playAgainButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        menuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }
}
