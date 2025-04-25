using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    GameManager gameManager;
    LevelManager levelManager;
    private void Start()
    {
        levelManager = LevelManager.Instance;
        gameManager = GameManager.Instance;
    }
 
    public void StartLevel()
    {
        Debug.Log("start level");//+
        if (!string.IsNullOrWhiteSpace(gameManager.PlayerName))
        { 
            gameManager.StartGame();//+
            ActionController.OnLevelStart?.Invoke();//-
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
            ActionController. OnLevelRestarted?.Invoke();
        }
        else
        {
            ActionController.OnPopUpOpened?.Invoke();

        }
    }

}


public static partial class ActionController
{
    public static Action OnLevelStart; 
}
