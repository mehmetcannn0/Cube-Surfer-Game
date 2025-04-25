using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public string PlayerName { get; private set; }
    public int Score { get; private set; }
    public int Gold { get; private set; }

    PlayerMovementManager playerMovementManager;
    PlayerInteractionController playerInteractionController;

    void Start()
    {
        playerInteractionController = PlayerInteractionController.Instance;
        playerMovementManager = PlayerMovementManager.Instance;

        playerInteractionController.OnScoreAdded += AddScore;
        playerInteractionController.OnGameOver += GameOver;
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += IncreaseGold;
     
        LevelManager.Instance.OnLevelFinished += FinishLevel;
        LevelManager.Instance.OnLevelRestarted += StartGame;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= IncreaseGold;

        LevelManager.Instance.OnLevelFinished -= FinishLevel;
        LevelManager.Instance.OnLevelRestarted -= StartGame;
    }

    private void OnDestroy()
    {
        playerInteractionController.OnScoreAdded -= AddScore;
        playerInteractionController.OnGameOver -= GameOver;
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
        //Debug.Log("NickName: " + playerName);
    }

}
 
