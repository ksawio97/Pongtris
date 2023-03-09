using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController: MonoBehaviour
{
    private int _ballCount = 0;

    [SerializeField]
    private ScoreHandler scoreHandler;

    public void BallCreated()
    {
        _ballCount += 1;
        CheckBallCount();
    }

    public void BallDestroyed()
    {
        _ballCount -= 1;
        CheckBallCount();
    }

    private void CheckBallCount()
    {
        Debug.Log(_ballCount);
        if (_ballCount <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log(scoreHandler.getPoints);
        SceneManager.LoadScene(3);
    }
}