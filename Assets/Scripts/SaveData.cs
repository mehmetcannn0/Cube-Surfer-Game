using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private const string PLAYER_DATA_FILE_NAME = "/PlayerData.json";
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
        string filePath = Application.persistentDataPath + PLAYER_DATA_FILE_NAME;
        System.IO.File.WriteAllText(filePath, leaderboardData);
        Debug.Log("data kaydedýldý");
        //Debug.Log(filePath);
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + PLAYER_DATA_FILE_NAME;
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
        newPlayer.Name = gameManager.PlayerName;
        newPlayer.Score = gameManager.Score;
        newPlayer.Gold = gameManager.Gold;
        Leaderboard.Players.Add(newPlayer);
        SaveToJson();
    }

}

[System.Serializable]
public class Leaderboard
{
    public List<Player> Players = new List<Player>();
}

[System.Serializable]
public class Player
{
    public string Name;
    public int Gold;
    public int Score;
}

