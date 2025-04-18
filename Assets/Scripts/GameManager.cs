using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    LevelManager levelManager;
    void Start()
    {
        levelManager = LevelManager.Instance;
        levelManager.StartLevel();

    }

    // Update is called once per frame
    void Update()
    {

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
