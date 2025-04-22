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

    private void Start()
    { 
        LoadFromJson();        
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
    public void AddDummyPlayers()
    {
        leaderboard.players.Clear();  
        leaderboard.players.Add(new Player { name = "Alice", gold = 120, score = 1500 });
        leaderboard.players.Add(new Player { name = "Bob", gold = 90, score = 1200 });
        leaderboard.players.Add(new Player { name = "Charlie", gold = 300, score = 1700 });
        leaderboard.players.Add(new Player { name = "Diana", gold = 50, score = 800 });
        leaderboard.players.Add(new Player { name = "Eve", gold = 200, score = 1000 });
        Debug.Log(leaderboard.players.Count);

        SaveToJson();  
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