using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int[] allowedXPositions = { -4, -2, 0, 2, 4 };
    private float groundlength;
    private int coinCountInGround = 200;
    private int spaceSize = 20;

    private List<GameObject> walls = new List<GameObject>();
    private List<GameObject> cubes = new List<GameObject>();
    private List<GameObject> coins = new List<GameObject>();

    [SerializeField] GameObject player;
    [SerializeField] Transform ground;
    [SerializeField] Transform wallsParents;
    [SerializeField] Transform coinsParents;
    [SerializeField] Transform playerVisualTransform;

    public RectTransform targetCoinUI;
    public Transform cubesParentOnGround;
    public Canvas canvas;

    PlayerMovementManager playerMovementManager;
    LeaderboardManager leaderboardManager;
    PrefabManager prefabManager;
    GameManager gameManager;
    GameObject finishGroupGround;
    UIManager uiManager;
    SaveData saveData;
    public enum LevelDirection
    {
        Forward,
        Left,
        Right
    }

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
        playerMovementManager = PlayerMovementManager.Instance;
        leaderboardManager = LeaderboardManager.Instance;
        prefabManager = PrefabManager.Instance;
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        saveData = SaveData.Instance;
    }

    public void CreateLevel()
    {
        Vector3 groundForward = Vector3.forward;
        Vector3 groundLeft = Vector3.left;
        Vector3 groundRight = Vector3.right; 
        player.transform.position = Vector3.zero;
        playerVisualTransform.position = Vector3.zero;
        groundlength = ground.transform.localScale.z;

        CreateGround(groundlength);

        for (int i = 1; i < groundlength / 20; i++)
        {
            int cubeOrWall = Random.Range(0, 2);
            if (cubeOrWall != 0 || i < 4)
            {
                CreateCube(i, LevelDirection.Forward, Vector3.zero);
                CreateCube(i, LevelDirection.Left, new Vector3(0, 0, groundlength));
                CreateCube(i, LevelDirection.Right, new Vector3(-groundlength, 0, groundlength));
            }
            else
            {
                CreateWall(i, LevelDirection.Forward, Vector3.zero);
                CreateWall(i, LevelDirection.Left, new Vector3(0, 0, groundlength));
                CreateWall(i, LevelDirection.Right, new Vector3(-groundlength, 0, groundlength));
            }
        }
        CreateCoin(LevelDirection.Forward);
        CreateCoin(LevelDirection.Left);
        CreateCoin(LevelDirection.Right);
    }
    private void CreateCube(int index, LevelDirection direction, Vector3 startOffset)
    {


        for (int j = 0; j < 5; j++)
        {
            int randomCubeSize = Random.Range(0, 4);
            if (randomCubeSize == 0) continue;

            Vector3 basePos = GetObjectPosition(index, j, direction, startOffset);
            GameObject newCubeObj = Instantiate(prefabManager.cubePrefab, basePos, Quaternion.identity, cubesParentOnGround);
            cubes.Add(newCubeObj);

            Cube newCube = newCubeObj.GetComponent<Cube>();
            newCube.cubeSize = randomCubeSize;

            for (int k = 1; k < randomCubeSize; k++)
            {
                Vector3 stackPos = basePos + Vector3.up * k * 2;
                GameObject cube = Instantiate(prefabManager.cubePrefab, stackPos, Quaternion.identity, cubesParentOnGround);
                newCube.cubeList.Add(cube.GetComponent<Cube>());
                cubes.Add(cube);
            }
        }
    }

    private void CreateWall(int index, LevelDirection direction, Vector3 startOffset)
    {

        for (int j = 0; j < 5; j++)
        {
            int randomWallSize = Random.Range(1, 4);

            Vector3 basePos = GetObjectPosition(index, j, direction, startOffset);
            GameObject newWallObj = Instantiate(prefabManager.wallPrefab, basePos, Quaternion.identity, wallsParents);
            walls.Add(newWallObj);

            newWallObj.GetComponent<Wall>().wallSize = randomWallSize;

            for (int k = 1; k < randomWallSize; k++)
            {
                Vector3 stackPos = basePos + Vector3.up * k * 2;
                GameObject wall = Instantiate(prefabManager.wallPrefab, stackPos, Quaternion.identity, newWallObj.transform);
                walls.Add(wall);
            }
        }
    }

    private void CreateCoin(LevelDirection direction)
    {
        for (int i = 0; i < coinCountInGround; i++)
        {
            int randomX = allowedXPositions[Random.Range(0, allowedXPositions.Length)];
            int randomZ;
            do
            {
                randomZ = Random.Range(0, (int)groundlength);
            } while (randomZ % 10 == 0);

            Vector3 spawnPosition = GetCoinPosition(randomX, randomZ, direction);
            GameObject newCoin = Instantiate(prefabManager.coinPrefab, spawnPosition, Quaternion.identity, coinsParents);
            coins.Add(newCoin);
        }
    }

    private Vector3 GetObjectPosition(int index, int j, LevelDirection direction, Vector3 startOffset)
    {
        int horizontalPosition = -4;
        switch (direction)
        {
            case LevelDirection.Forward:
                return new Vector3(horizontalPosition + j * 2, 0, index * spaceSize) + startOffset;
            case LevelDirection.Left:
                return new Vector3(-index * spaceSize, 0, horizontalPosition + j * 2) + startOffset;
            case LevelDirection.Right:
                return new Vector3(horizontalPosition + j * 2, 0, index * spaceSize) + startOffset;
            default:
                return Vector3.zero;
        }
    }

    private Vector3 GetCoinPosition(int x, int z, LevelDirection direction)
    {
        switch (direction)
        {
            case LevelDirection.Forward:
                return new Vector3(x, 0, z);
            case LevelDirection.Left:
                return new Vector3(-z, 0, x + groundlength);
            case LevelDirection.Right:
                return new Vector3(x - groundlength, 0, z + groundlength);
            default:
                return Vector3.zero;
        }
    }

    public void StartLevel()
    {
        if (gameManager.playerName != "" && gameManager.playerName != null)
        {
            gameManager.StartGame();
            uiManager.DeactiveUIs();
        } 
    }

    public void NextLevel()
    {
        DestroyObjects(walls);
        DestroyObjects(cubes);
        DestroyObjects(coins);
        Destroy(finishGroupGround);
        CreateLevel();
        playerMovementManager.RotatePlayer();
        gameManager.NextLevel();
        uiManager.MakeDeactiveUI(uiManager.finishLevelUI);         
    }

    public void FinishLEvel()
    {
        uiManager.MakeActiveUI(uiManager.finishLevelUI);
        gameManager.FinishLevel();
    }

    public void GameOver()
    {
        uiManager.MakeActiveUI(uiManager.playerNameUI);
        uiManager.MakeActiveUI(uiManager.leaderboardUI);
        SavePlayerData();
        GetAndUpdatePlayersData();
        leaderboardManager.UpdateLeaderboardUI();
        uiManager.MakeActiveUI(uiManager.gameOverUI);
        //GetAndUpdatePlayersData();
        gameManager.GameOver();

    }

    public void RestartLevel()
    {
        if (gameManager.playerName != "" && gameManager.playerName != null)
        {
            playerMovementManager.PlayerDirectionSetForward();
            ClearLevel();
            CreateLevel();
            gameManager.StartGame();
            uiManager.DeactiveUIs();
        }
    }

    private void CreateGround(float localScaleZ)
    {
        finishGroupGround = Instantiate(prefabManager.finishGroup, new Vector3(-localScaleZ, -0.5f, 2 * localScaleZ + 15), Quaternion.identity);
    }

    private void SavePlayerData()
    {
        Player newPlayer = new Player();
        newPlayer.name = gameManager.playerName;
        newPlayer.score = gameManager.score;
        newPlayer.gold = gameManager.gold;
        saveData.leaderboard.players.Add(newPlayer);
        saveData.SaveToJson();
    }

    private void GetAndUpdatePlayersData()
    {
        saveData.LoadFromJson();
    }

    private void ClearLevel()
    {
        DestroyObjects(walls);
        DestroyObjects(cubes);
        DestroyObjects(coins);
        Destroy(finishGroupGround);
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

