using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string playerName;
    public int playerXP;
    public float playerHealth;

    public void Save(string filePath)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Player"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Player");
        }
        string jsonData = JsonUtility.ToJson(this, true);
        File.WriteAllText(filePath, jsonData);
    }

    public void Load()
    {
        string filePath = Application.persistentDataPath + "/Player/player.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            PlayerInfo info = JsonUtility.FromJson<PlayerInfo>(jsonData);
            playerName = info.playerName;
            playerXP = info.playerXP;
            playerHealth = info.playerHealth;
        }
    }
}
