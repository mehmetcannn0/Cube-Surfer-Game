using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public string PlayerName { get; private set; }
    public int Score { get; private set; }
    public int Gold { get; private set; }

    PlayerMovementManager playerMovementManager;

    void Start()
    { 
        playerMovementManager = PlayerMovementManager.Instance;
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += IncreaseGold;
     
        ActionController.OnScoreAdded += AddScore;
        ActionController.OnGameOver += GameOver;
        ActionController.OnLevelFinished += FinishLevel;
        ActionController.OnLevelRestarted += StartGame;
        ActionController.OnNextLevelStarted += NextLevel;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= IncreaseGold;

        ActionController.OnLevelFinished -= FinishLevel;
        ActionController.OnLevelRestarted -= StartGame;
        ActionController.OnScoreAdded -= AddScore;
        ActionController.OnGameOver -= GameOver;
        ActionController.OnNextLevelStarted -= NextLevel;
    }

    public void StartGame()
    {
        Gold = 0;
        Score = 0; 
        playerMovementManager.RunPlayer();
    }

    public void IncreaseGold()
    {
        Gold++;
    }

    public void AddScore(int addScore)
    {     
        Score += addScore;
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
        PlayerName = value;
    }
}
 
