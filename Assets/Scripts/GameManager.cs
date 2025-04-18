using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    LevelManager levelManager;
    void Start()
    {
        levelManager = LevelManager.Instance;
        levelManager.StartLevel();

    }
 
    public void StartGame()
    {

    }
    public void GameOver()
    {

    }

    public void RestartGame()
    {

    }

}
