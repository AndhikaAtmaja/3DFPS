using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static SaveData _saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public BestScoreSaveData BestScoreData;
        public AudioSaveData AudioData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/saveGame" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandelSave();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(_saveData, true));
    }

    private static void HandelSave()
    {
        if (ScoreManager.instance != null)
            ScoreManager.instance.Save(ref _saveData.BestScoreData);

        if (ScoreManager.instance != null)
            SoundSettings.instance.Save(ref _saveData.AudioData);
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());
        _saveData = JsonUtility.FromJson<SaveData>(saveContent);
        HandelLoad();
    }

    private static void HandelLoad()
    {
        if (ScoreManager.instance != null)
            ScoreManager.instance.Load(_saveData.BestScoreData);

        if (SoundSettings.instance != null)
            SoundSettings.instance.Load(_saveData.AudioData);
    }

    public static BestScoreSaveData GetBestScore()
    {
        Debug.Log("File" + SaveFileName());
        if (!File.Exists(SaveFileName()))
            return new BestScoreSaveData(); // default 0

        string json = File.ReadAllText(SaveFileName());
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data.BestScoreData;
    }
}
