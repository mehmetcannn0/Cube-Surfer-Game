using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    GameManager gameManager;
    LevelManager levelManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        levelManager = LevelManager.Instance;
    }

    public void StartLevel()
    {
        if (!string.IsNullOrWhiteSpace(gameManager.PlayerName))
        {
            gameManager.StartGame();
            ActionController.OnLevelStart?.Invoke();
        }
        else
        {
            ActionController.OnPopUpOpened?.Invoke();
        }
    }
    public void RestartLevel()
    {
        if (!string.IsNullOrWhiteSpace(gameManager.PlayerName))
        {
            ActionController.OnLevelRestarted?.Invoke();
        }
        else
        {
            ActionController.OnPopUpOpened?.Invoke();
        }
    }
    public void NextLevel()
    {
        levelManager.ClearLevel();
        levelManager.CreateLevel();
        ActionController.OnNextLevelStarted?.Invoke();
    }

}

public static partial class ActionController
{
    public static Action OnLevelStart;
}
