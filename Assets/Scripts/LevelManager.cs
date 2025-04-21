using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int[] allowedXPositions = { -4, -2, 0, 2, 4 };
    int coinCountInLevel = 200;
    float localScaleZ;
    int spaceSize = 20;

    [SerializeField] Transform ground;
    [SerializeField] GameObject player;
    [SerializeField] Transform wallsParents;
    [SerializeField] Transform coinsParents;
    [SerializeField] Transform playerVisualTransform;


    public Transform cubesParentOnGround;
    public Canvas canvas;
    public RectTransform targetCoinUI;
    public List<GameObject> walls;
    public List<GameObject> cubes;
    public List<GameObject> coins;

    GameObject finishGroupGround;

    PrefabManager prefabManager;
    GameManager gameManager;
    UIManager uiManager;

    public static LevelManager Instance;
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
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;
        prefabManager = PrefabManager.Instance;
    }

    public void CreateLevel()
    {
        //player = Instantiate(prefabManager.playerPrefab,Vector3.zero,Quaternion.identity);
        player.transform.position = Vector3.zero;
        playerVisualTransform.position = Vector3.zero;


        localScaleZ = ground.transform.localScale.z;
        CreateGround(localScaleZ);
        for (int i = 1; i < localScaleZ / 20; i++)
        {
            int cubeOrWall = UnityEngine.Random.Range(0, 2);
            if (cubeOrWall != 0 || i < 4)
            {
                CreateCube(i);
            }
            else
            {
                CreateWall(i);
            }
        }
        CreateCoin();
    }

    private void CreateCube(int i)
    {
        int xPosition = -4;
        for (int j = 0; j < 5; j++)
        {
            int randomCubeSize = UnityEngine.Random.Range(0, 4);
            if (randomCubeSize != 0)
            {
                GameObject newCubeObj = Instantiate(prefabManager.cubePrefab, new Vector3(xPosition + j * 2, 0, i * spaceSize), Quaternion.identity, cubesParentOnGround);
                cubes.Add(newCubeObj);
                Cube newCube = newCubeObj.GetComponent<Cube>();
                newCube.cubeSize = randomCubeSize;
                for (int k = 1; k < randomCubeSize; k++)
                {
                    GameObject cube = Instantiate(prefabManager.cubePrefab, new Vector3(xPosition + j * 2, k * 2, i * spaceSize), Quaternion.identity, cubesParentOnGround);
                    newCube.cubeList.Add(cube.GetComponent<Cube>());
                    cubes.Add(cube);
                }
            }

        }
    }

    private void CreateWall(int i)
    {
        int xPosition = -4;
        for (int j = 0; j < 5; j++)
        {
            int randomWallSize = UnityEngine.Random.Range(1, 4);
            GameObject newWallObj = Instantiate(prefabManager.wallPrefab, new Vector3(xPosition + j * 2, 0, i * spaceSize), Quaternion.identity, wallsParents);
            walls.Add(newWallObj);
            newWallObj.GetComponent<Wall>().wallSize = randomWallSize;
            for (int k = 1; k < randomWallSize; k++)
            {
                GameObject wall = Instantiate(prefabManager.wallPrefab, new Vector3(xPosition + j * 2, k * 2, i * spaceSize), Quaternion.identity, newWallObj.transform);
                walls.Add(wall);

            }
        }

    }

    private void CreateCoin()
    {

        for (int i = 0; i < coinCountInLevel; i++)
        {

            int randomX = allowedXPositions[UnityEngine.Random.Range(0, allowedXPositions.Length)];
            int randomZ;
            do
            {
                randomZ = UnityEngine.Random.Range(0, (int)localScaleZ);
            } while (randomZ % 10 == 0);

            Vector3 spawnPosition = new Vector3(randomX, 0, randomZ);
            GameObject newCoin = Instantiate(prefabManager.coinPrefab, spawnPosition, Quaternion.identity, coinsParents);
            coins.Add(newCoin);
            //newCoin.collectCoinAction += gameEvents.CollectCoin;
        }
    }

    public void StartLevel()
    {
        gameManager.StartGame();
    }

    public void NextLevel()
    {
        if (gameManager.playerName != "" && gameManager.playerName != null)
        {
            DestroyObjects(walls);
            DestroyObjects(cubes);
            DestroyObjects(coins);
            Destroy(finishGroupGround);
            CreateLevel();
            gameManager.NextLevel();
            uiManager.playerNameUI.SetActive(false);
            uiManager.finishLevelUI.SetActive(false);   

        }
        else
        {

            uiManager.playerNameUI.SetActive(true);

        }

    }

    public void FinishLEvel()
    {
        uiManager.finishLevelUI.SetActive(true);
        gameManager.FinishLevel();
    }

    public void GameOver()
    {
        uiManager.gameOverUI.SetActive(true);
        uiManager.playerNameUI.SetActive(true);

        gameManager.GameOver();
    }

    public void RestartLevel()
    {
        if (gameManager.playerName != "" && gameManager.playerName != null)
        {
            Debug.Log(gameManager.playerName);
            DestroyObjects(walls);
            DestroyObjects(cubes);
            DestroyObjects(coins);
            Destroy(finishGroupGround);
            CreateLevel();
            gameManager.StartGame();
            uiManager.playerNameUI.SetActive(false);
            uiManager.gameOverUI.SetActive(false);

        }
        else
        {
            uiManager.playerNameUI.SetActive(true);

        }

    }

    private void CreateGround(float localScaleZ)
    {

        finishGroupGround = Instantiate(prefabManager.finishGroup, new Vector3(0, -0.5f, localScaleZ + 15), Quaternion.identity);
    }
    public void DestroyObjects(List<GameObject> list)
    {
        foreach (GameObject obj in list)
        {
            if (obj != null) Destroy(obj);
        }
        list.Clear();
    }
}

