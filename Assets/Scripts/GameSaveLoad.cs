using UnityEngine;
using System.IO;
using System.Text;

public static class GameSaveLoad
{
    static readonly string path = Application.persistentDataPath + "/SaveData.json";
    static readonly string highScorePath = Application.persistentDataPath + "/HighScore.txt";
    static readonly Encoding encodeType = Encoding.ASCII;

    public static int score;
    public static int highScore = GetHighScore();

    public static void Save()
    {
        var saveData = CreateSaveData();

        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json, encodeType);
    }

    private static SaveData CreateSaveData()
    {
        SaveData saveData = new();

        var gameObjects = Object.FindObjectsOfType<GameObject>();
        saveData.points = score;
        foreach (var gameObject in gameObjects )
        {
            switch (gameObject.tag)
            {
                case "Player":
                    saveData.playerPos = gameObject.transform.position;
                    break;
                case "Ball":
                    saveData.ballPacks.Add(new BallPack { pos = gameObject.transform.position, moveAmount = gameObject.GetComponent<BallController>().moveAmount});
                    break;
                case "BoxesManager":
                    saveData.boxesManagerPack = gameObject.GetComponent<BoxesManager>().GetSaveData();
                    break;
            }
        }
        //TO DO add checking points with highscore and then maybe changing it (maybe add it to DeleteSave ?)
        return saveData;
    }

    public static SaveData Load()
    {
        if (!File.Exists(path))
            return null;
        
        var json = File.ReadAllText(path, encodeType);
        var saveData = JsonUtility.FromJson<SaveData>(json);

        return saveData;
    }

    private static int GetHighScore()
    {
        if (!File.Exists(highScorePath))
            return 0;

        var num = File.ReadAllText(highScorePath, encodeType);

        return int.Parse(num);
    }

    private static void UpdateHighScore()
    {
        if (highScore >= score)
            return;

        highScore = score;

        File.WriteAllText(highScorePath, highScore.ToString(), encodeType);
    }
    public static void DeleteSave()
    {
        UpdateHighScore();
        if (!File.Exists(path))
            return;

        File.Delete(path);
    }
}
