using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    private const int COIN_COUNT_IN_GROUND = 200;
    private const int SPACE_SIZE = 20;
    private const int CUBE_WIDTH = 2;

    private int[] allowedXPositions = { -4, -2, 0, 2, 4 };
    private float groundlength;

    private List<GameObject> walls = new List<GameObject>();
    private List<GameObject> cubes = new List<GameObject>();
    private List<GameObject> coins = new List<GameObject>();
    private GameObject finishGroupGround;

    [SerializeField] GameObject player;
    [SerializeField] Transform ground;
    [SerializeField] Transform wallsParents;
    [SerializeField] Transform coinsParents;
    [SerializeField] Transform playerVisualTransform;

    PrefabManager prefabManager;

    public Transform CubesParentOnGround;

    public enum LevelDirection
    {
        Forward,
        Left,
        Right
    }

    private void Start()
    {
        prefabManager = PrefabManager.Instance;
        CreateLevel();
    }

    private void OnEnable()
    {
        ActionController.OnLevelRestarted += ClearLevel;
        ActionController.OnLevelRestarted += CreateLevel;
        ActionController.OnNextLevelStarted += ClearLevel;
        ActionController.OnNextLevelStarted += CreateLevel;
    }

    private void OnDisable()
    {
        ActionController.OnLevelRestarted -= ClearLevel;
        ActionController.OnLevelRestarted -= CreateLevel;
        ActionController.OnNextLevelStarted -= ClearLevel;
        ActionController.OnNextLevelStarted -= CreateLevel;
    }

    public void CreateLevel()
    {
        player.transform.position = Vector3.zero;
        playerVisualTransform.position = Vector3.zero;
        groundlength = ground.transform.localScale.z;

        CreateFinishGround(groundlength);

        for (int i = 1; i < groundlength / 20; i++)
        {
            int cubeOrWall = UnityEngine.Random.Range(0, 2);
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
            int randomCubeSize = UnityEngine.Random.Range(0, 4);
            if (randomCubeSize == 0) continue;

            Vector3 basePos = GetObjectPosition(index, j, direction, startOffset);
            CubeTower cubeTower = prefabManager.InstantiateObjet(prefabType: PrefabType.CubeTower, objectPosition: basePos, parent: CubesParentOnGround).GetComponent<CubeTower>();

            cubeTower.CubeSize = randomCubeSize;
            cubes.Add(cubeTower.gameObject);

            for (int k = 0; k < randomCubeSize; k++)
            {
                Vector3 stackPos = basePos + Vector3.up * k * CUBE_WIDTH;
                GameObject cube = prefabManager.InstantiateObjet(prefabType: PrefabType.Cube, objectPosition: stackPos, parent: cubeTower.transform);
                cubeTower.CubeList.Add(cube);
                cubes.Add(cube);
            }
        }
    }

    private void CreateWall(int index, LevelDirection direction, Vector3 startOffset)
    {

        for (int j = 0; j < 5; j++)
        {
            int randomWallSize = UnityEngine.Random.Range(1, 4);

            Vector3 basePos = GetObjectPosition(index, j, direction, startOffset);
            GameObject wallTower = prefabManager.InstantiateObjet(prefabType: PrefabType.WallTower, objectPosition: basePos, parent: wallsParents);
            wallTower.GetComponent<WallTower>().WallSize = randomWallSize;

            walls.Add(wallTower);

            for (int k = 0; k < randomWallSize; k++)
            {
                Vector3 stackPos = basePos + Vector3.up * k * CUBE_WIDTH;
                GameObject wall = prefabManager.InstantiateObjet(prefabType: PrefabType.Wall, objectPosition: stackPos, parent: wallTower.transform);

                walls.Add(wall);
            }
        }
    }

    private void CreateCoin(LevelDirection direction)
    {
        for (int i = 0; i < COIN_COUNT_IN_GROUND; i++)
        {
            int randomX = allowedXPositions[UnityEngine.Random.Range(0, allowedXPositions.Length)];
            int randomZ;
            do
            {
                randomZ = UnityEngine.Random.Range(0, (int)groundlength);
            } while (randomZ % 10 == 0);

            Vector3 spawnPosition = GetCoinPosition(randomX, randomZ, direction);
            GameObject newCoin = prefabManager.InstantiateObjet(prefabType: PrefabType.Coin, objectPosition: spawnPosition, parent: coinsParents);

            coins.Add(newCoin);
        }
    }

    private Vector3 GetObjectPosition(int index, int j, LevelDirection direction, Vector3 startOffset)
    {
        int horizontalPosition = -4;
        switch (direction)
        {
            case LevelDirection.Forward:
                return new Vector3(horizontalPosition + j * CUBE_WIDTH, 0, index * SPACE_SIZE) + startOffset;
            case LevelDirection.Left:
                return new Vector3(-index * SPACE_SIZE, 0, horizontalPosition + j * CUBE_WIDTH) + startOffset;
            case LevelDirection.Right:
                return new Vector3(horizontalPosition + j * CUBE_WIDTH, 0, index * SPACE_SIZE) + startOffset;
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

    private void CreateFinishGround(float localScaleZ)
    {
        finishGroupGround = prefabManager.InstantiateObjet(prefabType: PrefabType.FinishGroup, objectPosition: new Vector3(-localScaleZ, -0.5f, 2 * localScaleZ + 15));
    }

    public void ClearLevel()
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

public static partial class ActionController
{
    public static Action OnNextLevelStarted;
    public static Action OnLevelRestarted;
    public static Action OnLevelFinished;
}
