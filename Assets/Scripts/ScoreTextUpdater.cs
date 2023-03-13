using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreTextUpdater : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highScore;

    [SerializeField]
    TextMeshProUGUI score;

    private void Start()
    {
        highScore.text = $"High Score: {GameSaveLoad.highScore}";
        score.text = $"Score: {GameSaveLoad.score}";
    }
}
