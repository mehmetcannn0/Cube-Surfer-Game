using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{ 
    public Leaderboard leaderboard = new Leaderboard();
    public static SaveData Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    } 

    public void SaveToJson()
    {
        string leaderboardData = JsonUtility.ToJson(leaderboard,true);
        Debug.Log("string "  + leaderboardData);
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        System.IO.File.WriteAllText(filePath, leaderboardData);
        Debug.Log("data kaydedýldý"); 
        //Debug.Log(filePath);
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        if (System.IO.File.Exists(filePath))
        {
            string jsonData = System.IO.File.ReadAllText(filePath);
            leaderboard = JsonUtility.FromJson<Leaderboard>(jsonData); 
            Debug.Log("data çekildi");
        }
        else
        {
            Debug.Log("Kayýt dosyasý bulunamadý.");
        }
    }
}

[System.Serializable]
public class Leaderboard
{
    public List<Player> players = new List<Player>();
}

[System.Serializable]
public class Player
{
    public string name;
    public int gold;
    public int score;
}