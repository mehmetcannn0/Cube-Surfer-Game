using DG.Tweening;
using System;
using UnityEngine;

public class PlayerInteractionController :  MonoSingleton<PlayerInteractionController>
{
    private bool isGameOver;
    public Transform CubeParent;
    public Transform PlayerVisualTransform;

    LevelManager levelManager;

    //protected override void Awake()
    //{
    //    base.Awake();

    //}
    private void Start()
    {
        levelManager = LevelManager.Instance; 
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

        if (collision.gameObject.TryGetComponent(out IFinishLevel finishLevel))
        {
            finishLevel.FinishLevel();
            isGameOver = false;
        }
    }

    private void IsCollisionForward(Collision collision, Vector3 directionToContact)
    {
        if (Vector3.Dot(transform.forward, directionToContact) > Utils.COLLISION_TRESHOLD)
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
                ToggleIsGameOver();
                SaveData.Instance.SavePlayerData();
                ActionController.OnGameOver?.Invoke();
            }
            else
            { 
                ToggleIsGameOver();
            }
        }
    }

    private void OnStackableInteraction(IStackable stackable)
    {

        int cubeSize = stackable.OnStack();
        ActionController.OnScoreAdded?.Invoke(cubeSize);

        PlayerVisualTransform.localPosition = PlayerVisualTransform.localPosition + (Utils.CUBE_WIDTH * cubeSize * Vector3.up);

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
            cube.localPosition = childLocalPosition + (Utils.CUBE_WIDTH * Vector3.down * wallSize);

            transform.position = collision.gameObject.transform.position + (2.5f * wallSize * Vector3.up);
        }

        if (wallSize == childCount)
        {
            transform.position = collision.gameObject.transform.position + Vector3.up;

            PlayerVisualTransform.DOLocalMoveY((playerVisualPosition.y - (Utils.CUBE_WIDTH * wallSize)), 1.5f);

            return;
        }

        PlayerVisualTransform.localPosition += Utils.CUBE_WIDTH * Vector3.down * wallSize;
    }

    public void ToggleIsGameOver()
    {
        isGameOver = !isGameOver;
    }
}

public static partial class ActionController
{
    public static Action OnGameOver;
    public static Action<int> OnScoreAdded;
}
