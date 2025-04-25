using DG.Tweening;
using System;
using UnityEngine;

public class PlayerInteractionController : MonoSingleton<PlayerInteractionController>
{
    public Transform CubeParent;
    public Transform PlayerVisualTransform;
    private bool isGameOver;

    LevelManager levelManager; 

    public static PlayerInteractionController Instance;

    public event Action<int> OnScoreAdded;
    public event Action OnGameOver;

    private void Start()
    {
        levelManager = LevelManager.Instance;
        //gameManager = GameManager.Instance;
        //uiManager = UIManager.Instance; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IRotate rotate))
        {
            rotate.RotatePlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(); 
        }

        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 directionToContact = (contactPoint - transform.position).normalized;

        IsCollisionForward(collision, directionToContact);

        if (collision.gameObject.TryGetComponent(out IFinishLevel finishLevel))//interface
        { 
            finishLevel.FinishLevel();
        }
    }

    private void IsCollisionForward(Collision collision, Vector3 directionToContact)
    { 
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
    }

    private void OnObstacleInteraction(Collision collision, IObstacle obstacle)
    {
        int wallSize = obstacle.OnHit();
        int childCount = CubeParent.childCount;

        if (childCount >= wallSize)
        {
            PushCubesInCubesParent(wallSize);
            SetNewPosition(collision, wallSize, childCount);
        }
        else
        { 
            if (!isGameOver)
            {
                isGameOver = !isGameOver;
                SaveData.Instance.SavePlayerData();
                OnGameOver?.Invoke();
            }
            else
            {
                isGameOver = !isGameOver;
            }
        }
    }

    private void OnStackableInteraction(IStackable stackable)
    {
        int cubeSize = stackable.OnStack();
        
        PlayerVisualTransform.localPosition = PlayerVisualTransform.localPosition + (2 * cubeSize * Vector3.up);

        OnScoreAdded?.Invoke(cubeSize);
    }

    private void PushCubesInCubesParent(int wallSize)
    {
        for (int i = 0; i < wallSize; i++)
        {
            Transform lowerCube = CubeParent.GetChild(0);
            lowerCube.parent = levelManager.CubesParentOnGround;
        }
    }

    private void SetNewPosition(Collision collision, int wallSize, int childCount)
    {
        Vector3 playerVisualPosition = PlayerVisualTransform.localPosition;

        for (int i = 0; i < childCount - wallSize; i++)
        {
            Transform cube = CubeParent.GetChild(i);
            Vector3 childLocalPosition = cube.localPosition;
            cube.localPosition = childLocalPosition + (2 * Vector3.down * wallSize);

            transform.position = collision.gameObject.transform.position + (2.5f * wallSize * Vector3.up);
        }

        if (wallSize == childCount)
        {
            transform.position = collision.gameObject.transform.position + Vector3.up;

            PlayerVisualTransform.DOLocalMoveY((playerVisualPosition.y - (2 * wallSize)), 1.5f);
            
            return;
        }

        PlayerVisualTransform.localPosition += 2 * Vector3.down * wallSize;
    }
}
