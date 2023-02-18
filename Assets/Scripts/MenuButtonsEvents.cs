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
    void Start()
    {
        playButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        settingsButton.onClick.AddListener(() => { SceneManager.LoadScene(2); });
        //exitButton.onClick.AddListener();
    }
}
