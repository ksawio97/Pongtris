using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtonEvent : MonoBehaviour
{

    [SerializeField]
    Button pauseButton;

    [SerializeField]
    GameManager gameManager;

    void Start()
    {
        pauseButton.onClick.AddListener(() => { gameManager.TrySaveGame(); SceneManager.LoadScene(3);});
    }
}
