using UnityEngine;

public class GameManager : MonoBehaviour
{
      public string playerName { get; private set; }
     public  int score {  get; private set; }


    PlayerMovementManager playerMovementManager;
    LevelManager levelManager;
    public int gold {  get; private set; }

    public static GameManager Instance;

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

    private void OnEnable()
    {
        Coin.CollectCoinAction += IncreaseGold;
    }

    private void OnDisable()
    {
        Coin.CollectCoinAction -= IncreaseGold;
    }

    void Start()
    {
        playerMovementManager = PlayerMovementManager.Instance;
        levelManager = LevelManager.Instance;
        levelManager.CreateLevel();

    }
 
    public void StartGame()
    {
        Debug.Log("start game");
        playerMovementManager.RunPlayer();
    }
    
    public void IncreaseGold()
    {
        gold++;
    }
    
    public void FinishLevel()
    {
        playerMovementManager.StopPlayer();
    }
    
    public void NextLevel()
    {
        playerMovementManager.RunPlayer();

    }
    
    public void GameOver()
    {
        playerMovementManager.StopPlayer();

    }

    public void RestartGame()
    {
        playerMovementManager.RunPlayer();
    }
    public void UpdatePlayerName(string value)
    {
        playerName = value;
        Debug.Log("NickName: " + playerName);
    }

}
