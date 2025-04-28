using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    GameManager gameManager;
    public Leaderboard Leaderboard = new Leaderboard();
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
        gameManager = GameManager.Instance;
        ActionController.OnGameOver += LoadFromJson;
    }
 
    private void OnDestroy()
    {
        ActionController.OnGameOver -= LoadFromJson;

    }
    public void SaveToJson()
    {
        string leaderboardData = JsonUtility.ToJson(Leaderboard, true);
        //Debug.Log("string "  + leaderboardData);
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
            Leaderboard = JsonUtility.FromJson<Leaderboard>(jsonData);
            Debug.Log("data çekildi");
        }
        else
        {
            Debug.Log("Kayýt dosyasý bulunamadý.");
        }
    }
    public void SavePlayerData()
    {
        Player newPlayer = new Player();
        newPlayer.name = gameManager.PlayerName;
        newPlayer.score = gameManager.Score;
        newPlayer.gold = gameManager.Gold;
        Leaderboard.players.Add(newPlayer);
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

