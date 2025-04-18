using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform ground;
    [SerializeField] GameObject cubePrefab;
    [SerializeField] GameObject wallPrefab;
    [SerializeField] Transform wallsParents;
    public Transform cubesParentOnGround;

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

    public void StartLevel()
    {
        for (int i = 1; i < ground.transform.localScale.z / 20; i++)
        {
            int cubeOrWall = Random.Range(0, 2);
            if (cubeOrWall == 0)
            {
                CreateWall(i);
            }
            else
            {
                CreateCube(i);
            }
        }
    }

    private void CreateCube(int i)
    {
        int xPosition = -4;
        for (int j = 0; j < 5; j++)
        {
            int randomCubeSize = Random.Range(1, 4);
            GameObject newCubeObj = Instantiate(cubePrefab, new Vector3(xPosition + j * 2, 0, i * 20), Quaternion.identity,cubesParentOnGround);
            Cube newCube = newCubeObj.GetComponent<Cube>();
            newCube.cubeSize = randomCubeSize;
            for (int k = 1; k < randomCubeSize; k++)
            {
                GameObject cube = Instantiate(cubePrefab, new Vector3(xPosition + j * 2, k * 2, i * 20), Quaternion.identity,cubesParentOnGround);
                newCube.cubeList.Add(cube.GetComponent<Cube>());

            }
        }
    }

    private void CreateWall(int i)
    {
        int xPosition = -4;
        for (int j = 0; j < 5; j++)
        {
            int randomWallSize = Random.Range(1, 4);
            GameObject newWallObj = Instantiate(wallPrefab, new Vector3(xPosition + j * 2, 0, i * 20), Quaternion.identity, wallsParents);
            newWallObj.GetComponent<Wall>().wallSize = randomWallSize;
            for (int k = 1; k < randomWallSize; k++)
            {
                Instantiate(wallPrefab, new Vector3(xPosition + j * 2, k * 2, i * 20), Quaternion.identity, newWallObj.transform);

            }
        }
    }
}
