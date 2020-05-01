using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Player.savedGamePaths[Player.currentGameSaveIndex];
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(player);
        SaveData saveData = new SaveData(playerData);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static SaveData LoadGame()
    {
        string path = Player.savedGamePaths[Player.currentGameSaveIndex];

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
