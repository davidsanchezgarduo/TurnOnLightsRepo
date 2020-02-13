using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class PlayerData{
    public int points;
    public int zombieskilled;
    public int unitsDeployed;
    public int lastlevel;
}

public class DataController : MonoBehaviour
{
    public static DataController instance;
    public string playerDataPath = "player.json";
    public PlayerData playerData;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/" + playerDataPath)) {
            string json = File.ReadAllText(Application.persistentDataPath + "/" + playerDataPath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else {
            playerData = new PlayerData();
            playerData.points = 0;
            playerData.zombieskilled = 0;
            playerData.unitsDeployed = 0;
            playerData.lastlevel = 0;
            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(Application.persistentDataPath + "/" + playerDataPath, json);
        }
    }

    public void SaveData() {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/" + playerDataPath, json);
    }

    public void PlusData(int _points, int _zombies, int _units, bool passLevel) {
        playerData.points += _points;
        playerData.zombieskilled += _zombies;
        playerData.unitsDeployed += _units;
        if (passLevel) {
            playerData.lastlevel++;
        }
        SaveData();

    }


}
