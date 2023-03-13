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
    void Start()
    {
        resumeButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        menuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        //exitButton.onClick.AddListener();
    }
}
