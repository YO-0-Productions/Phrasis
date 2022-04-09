using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/save.pd";

    public static void SavePlayerData(string sceneName, bool[] completions)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(sceneName, completions);

        formatter.Serialize(stream, data);
        stream.Close();


    }

    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else
        {
            PrepareNewGame();
            Debug.Log($"Save file not found in: {path}! Starting a new game");
            return LoadPlayerData();
        }
    }

    public static void PrepareNewGame()
    {
        string entryScene = "FirstRoom";
        bool[] completions = new bool[12];
        SaveSystem.SavePlayerData(entryScene, completions);
    }

    public static void SaveCompletion(int index)
    {
        PlayerData data = LoadPlayerData();
        data.completions[index] = true;
        SavePlayerData(data.sceneName, data.completions);
        EndGameIfCompleted();
    }

    private static bool IsCompleted(PlayerData data)
    {
        foreach (bool completion in data.completions)
        {
            if (!completion)
            {
                return false;
            }
        }
        return true;
    }

    private static void EndGameIfCompleted()
    {
        PlayerData data = LoadPlayerData();

        if (IsCompleted(data))
        {
            Debug.Log("Thanks for playing.");
            SceneManager.LoadScene("StartMenu");
        }
    }

    public static void SaveLocation(Scene current, Scene next)
    {
        string sceneName = next.name;
        if (sceneName != "StartMenu")
        {
            PlayerData data = LoadPlayerData();
            SavePlayerData(sceneName, data.completions);
        }
        
    }
}
