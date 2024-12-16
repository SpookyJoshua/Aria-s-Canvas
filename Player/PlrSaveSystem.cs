using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;


public class PlrSaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class PlrData
    {
        public string plrXP;
        public string plrHealth;
    }

    public PlrData loadedPlayer;

    private string filename = "playerData.json";

    private void OnApplicationQuit()
    {
        SaveData(loadedPlayer);
    }

    public bool checkIfPathExists()
    {
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        if (File.Exists(filePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveData(PlrData data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, filename);
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, jsonData);
    }

    public PlrData LoadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, filename);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            // Deserialize JSON manually
            loadedPlayer = JsonUtility.FromJson<PlrData>(jsonData);

            return loadedPlayer;
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
            return null;
        }
    }
}
