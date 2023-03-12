using UnityEngine;
using System.IO;
using System.Text;

public static class GameSaveLoad
{
    static readonly string path = Application.persistentDataPath + "/SaveData.json";
    static readonly Encoding encodeType = Encoding.ASCII;

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
        foreach(var gameObject in gameObjects )
        {
            switch (gameObject.tag)
            {
                case "Player":
                    saveData.playerPos = gameObject.transform.position;
                    break;
                case "Ball":
                    saveData.ballPacks.Add(new BallPack { pos = gameObject.transform.position, moveAmount = gameObject.GetComponent<BallController>().moveAmount});
                    break;
                case "Score":
                    saveData.points = gameObject.GetComponent<ScoreHandler>().getPoints;
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

    public static void DeleteSave()
    {
        File.Delete(path);
    }
}
