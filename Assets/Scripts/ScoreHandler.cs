using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private TextMeshProUGUI score;

    public void AddPoints(int points)
    {
        GameSaveLoad.score += points;
        score.text = GameSaveLoad.score.ToString();
    }

    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();

        GameSaveLoad.score = 0;
        score.text = GameSaveLoad.score.ToString();
    }
}
