using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform cubeParent;
    [SerializeField] private Transform playerVisualTransform;

    LevelManager levelManager;
    private void Start()
    {
        levelManager = LevelManager.Instance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            OnCollectableInteraction(collectable);
            return;
        }

        if (collision.gameObject.TryGetComponent(out IObstacle obstacle))
        {
            OnObstacleInteraction(collision, obstacle);
            return;
        }

    }

    private void OnObstacleInteraction(Collision collision, IObstacle obstacle)
    {
        int wallSize = obstacle.OnHit();
        int childCount = cubeParent.childCount;

        if (childCount >= wallSize)
        {
            PushCubesInCubesParent(wallSize);
            SetNewPosition(collision, wallSize, childCount);
        }
        else
        {
            Debug.LogError("gameover");

        }
    }

    private void OnCollectableInteraction(ICollectable collectable)
    {
        int cubeSize = collectable.OnCollect(cubesParentsTransform: cubeParent, upperCubePosition: playerVisualTransform.localPosition, cubeIndex: 0);
        playerVisualTransform.localPosition = playerVisualTransform.localPosition + (2 * Vector3.up);
        for (int i = 1; i < cubeSize; i++)
        {
            collectable.OnCollect(cubesParentsTransform: cubeParent, upperCubePosition: playerVisualTransform.localPosition, cubeIndex: i);
            playerVisualTransform.localPosition = playerVisualTransform.localPosition + (2 * Vector3.up);
        }
    }

    private void PushCubesInCubesParent(int wallSize)
    {
        for (int i = 0; i < wallSize; i++)
        {
            Transform lowerCube = cubeParent.GetChild(0);
            lowerCube.parent = levelManager.cubesParentOnGround;

        }
    }
    private void SetNewPosition(Collision collision, int wallSize, int childCount)
    {
        Vector3 playerVisualPosition = playerVisualTransform.localPosition;


        for (int i = 0; i < childCount - wallSize; i++)
        {
            Transform cube = cubeParent.GetChild(i);
            Vector3 childLocalPosition = cube.localPosition;
            cube.localPosition = childLocalPosition + (2 * Vector3.down * wallSize);

            transform.position = collision.gameObject.transform.position + (2 * Vector3.up);
        }

        if (wallSize == 0)
        {
            playerVisualTransform.localPosition = playerVisualPosition + (2 * Vector3.down * 1);
        }

        playerVisualTransform.localPosition = playerVisualPosition + (2 * Vector3.down * wallSize); 
    }

}
