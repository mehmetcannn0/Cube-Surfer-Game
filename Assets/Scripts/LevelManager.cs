using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform ground;

    [SerializeField] Transform wallsParents;
    [SerializeField] Transform coinsParents;
    public Transform cubesParentOnGround;
    [SerializeField] private int coinCount = 200;
    private int[] allowedXPositions = { -4, -2, 0, 2, 4 };

    PrefabManager prefabManager;

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
        prefabManager =PrefabManager.Instance;
    }
    public void StartLevel()
    {
        for (int i = 1; i < ground.transform.localScale.z / 20; i++)
        {
            int cubeOrWall = Random.Range(0, 2);
            if (cubeOrWall != 0  || i <4)
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
            int randomCubeSize = Random.Range(0, 4);
            if (randomCubeSize != 0)
            {
                GameObject newCubeObj = Instantiate(prefabManager. cubePrefab, new Vector3(xPosition + j * 2, 0, i * 20), Quaternion.identity, cubesParentOnGround);
                Cube newCube = newCubeObj.GetComponent<Cube>();
                newCube.cubeSize = randomCubeSize;
                for (int k = 1; k < randomCubeSize; k++)
                {
                    GameObject cube = Instantiate(prefabManager.cubePrefab, new Vector3(xPosition + j * 2, k * 2, i * 20), Quaternion.identity, cubesParentOnGround);
                    newCube.cubeList.Add(cube.GetComponent<Cube>());

                }
            }

        }
    }

    private void CreateWall(int i)
    {
        int xPosition = -4;
        for (int j = 0; j < 5; j++)
        {
            int randomWallSize = Random.Range(1, 4);
            GameObject newWallObj = Instantiate(prefabManager.wallPrefab, new Vector3(xPosition + j * 2, 0, i * 20), Quaternion.identity, wallsParents);
            newWallObj.GetComponent<Wall>().wallSize = randomWallSize;
            for (int k = 1; k < randomWallSize; k++)
            {
                Instantiate(prefabManager.wallPrefab, new Vector3(xPosition + j * 2, k * 2, i * 20), Quaternion.identity, newWallObj.transform);
                
            }
        }

    }
    private void CreateCoin()
    {

        for (int i = 0; i < coinCount; i++)
        {

            int randomX = allowedXPositions[Random.Range(0, allowedXPositions.Length)];
            int randomZ;
            do
            {
                randomZ = Random.Range(0, 100);
            } while (randomZ % 10 == 0);

            Vector3 spawnPosition = new Vector3(randomX, 0, randomZ);
            Instantiate(prefabManager.coinPrefab, spawnPosition, Quaternion.identity,coinsParents);
        }
    }

    private void CreateGround(int i) { }

}
