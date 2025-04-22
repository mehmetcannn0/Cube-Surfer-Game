using DG.Tweening;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public Transform cubeParent;
    public Transform playerVisualTransform;

    PlayerMovementManager playerMovementManager;
    LevelManager levelManager;
    GameManager gameManager;

    public static PlayerInteractionController Instance;

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
        levelManager = LevelManager.Instance;
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rotate"))
        {
            Debug.Log("ground finish");

            playerMovementManager.RotatePlayer();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
            //GameAction.OnCollectCoin?.Invoke();
        }

        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 directionToContact = (contactPoint - transform.position).normalized;

        if (Vector3.Dot(transform.forward, directionToContact) > 0.7f)
        {
            if (collision.gameObject.TryGetComponent(out IStackable stackable))
            {
                OnStackableInteraction(stackable);
                return;
            }

            if (collision.gameObject.TryGetComponent(out IObstacle obstacle))
            {
                OnObstacleInteraction(collision, obstacle);
                return;
            }

        }
        if (collision.gameObject.CompareTag("finish"))
        {
            Debug.Log("level finish");
            levelManager.FinishLEvel();

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
            //Debug.LogError("gameover");
            levelManager.GameOver();

        }
    }

    private void OnStackableInteraction(IStackable stackable)
    {
        int cubeSize = stackable.OnStack(cubeIndex: 0);
        gameManager.AddScore(cubeSize);
        playerVisualTransform.localPosition = playerVisualTransform.localPosition + (2 * Vector3.up);
        for (int i = 1; i < cubeSize; i++)
        {
            stackable.OnStack(cubeIndex: i);
            playerVisualTransform.localPosition += 2 * Vector3.up;
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

            transform.position = collision.gameObject.transform.position + (2.5f * wallSize * Vector3.up);
        }

        if (wallSize == childCount)
        {
            playerVisualTransform.DOLocalMoveY((playerVisualPosition.y - (2 * wallSize)), 2f);
            return;
        }

        playerVisualTransform.localPosition += 2 * Vector3.down * wallSize;

    }

}
//    public static class GameAction{

//    public static Action OnCollectCoin;
//}
