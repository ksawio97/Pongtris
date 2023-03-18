using UnityEngine;
using System.IO;
using System.Text;

public static class GameSaveLoad
{
    public static readonly string path = Application.persistentDataPath + "/SaveData.json";
    public static readonly string highScorePath = Application.persistentDataPath + "/HighScore.txt";
    public static readonly string settingsPath = Application.persistentDataPath + "/SettingsData.txt";
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

        return saveData;
    }

    public static T Load<T>(string p)
    {
        if (!File.Exists(p))
            return default(T);
        
        var json = File.ReadAllText(p, encodeType);
        var saveData = JsonUtility.FromJson<T>(json);

        return saveData;
    }

    public static void DeleteSave()
    {
        UpdateHighScore();
        if (!File.Exists(path))
            return;

        File.Delete(path);
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

    public static void SaveSettings(VolumeSectionHandle[] volumeSectionHandles)
    {
        var settingsToSave = new SettingsData();

        foreach (var volumeSectionHandle in volumeSectionHandles)
        {
            AudioManager.Instance.ChangeVolumesOfType(volumeSectionHandle.type, volumeSectionHandle.sliderValue);
            settingsToSave.settingsData.Add(new SettingData { type = volumeSectionHandle.type, volume = volumeSectionHandle.sliderValue });
        }

        var json = JsonUtility.ToJson(settingsToSave);
        File.WriteAllText(settingsPath, json, encodeType);
    }
}
