using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    private int _ballCount = 0;

    [SerializeField]
    private ScoreHandler scoreHandler;

    [SerializeField]
    private EffectsController effectsController;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private BoxesManager boxesManager;

    void Start()
    {
        //setup game end condition
        OnDestroyActions.GameEnd = () => { GameOver(); };

        LoadGame();
    }
    public void TrySaveGame()
    {
        if (_ballCount <= 0)
            GameSaveLoad.DeleteSave();
        else
            GameSaveLoad.Save();
    }
    
    private void LoadGame()
    {
        var saveData = GameSaveLoad.Load<SaveData>(GameSaveLoad.path);
        //if no load
        if (saveData.IsUnityNull())
        {
            effectsController.CreateBall(new Vector3(0, 0));
            return;
        }
            
        //load balls
        foreach(var ballPack in saveData.ballPacks)
        {
            var ball = effectsController.CreateBall(ballPack.pos);
            ball.GetComponent<BallController>().moveAmount = ballPack.moveAmount;
        }
        //load player pos
        playerTransform.position = saveData.playerPos;

        //load score
        scoreHandler.AddPoints(saveData.points);

        //load BoxesManager
        boxesManager.LoadSaveData(saveData.boxesManagerPack);
    }

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
        if (_ballCount <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GameSaveLoad.DeleteSave();
        SceneManager.LoadScene(3);
    }
}
