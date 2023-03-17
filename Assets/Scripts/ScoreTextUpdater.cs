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
        if (highScore != null)
            highScore.text = $"High Score: {GameSaveLoad.highScore}";
        if (score != null)
            score.text = $"Score: {GameSaveLoad.score}";
    }
}
