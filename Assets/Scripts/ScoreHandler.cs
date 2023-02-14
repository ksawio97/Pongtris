using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private TextMeshProUGUI score;

    private int _points;

    public int pointsAdd
    { 
        set 
        { 
            _points += value;
            score.text = _points.ToString(); 
        } 
    }

    private void Start()
    {
        score = GetComponent<TextMeshProUGUI>();

        _points = 0;
        score.text = _points.ToString();
    }
}
