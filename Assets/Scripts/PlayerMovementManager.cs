using DG.Tweening;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private float newPositionHorizontalValue;
    private float HORIZONTAL_LIMIT_VALUE = 4;
    private float HORIZONTAL_MOVEMENT_SPEED = 0;
    private float FORWARD_MOVEMENT_SPEED = 0;
    private float ROTATION_ADNIMATION_TÝME = 1f;
    public enum PlayerDirection
    {
        Forward,
        Left,
        Right
    }

    public PlayerDirection direction;

    PlayerInputManager playerInputManager;

    public static PlayerMovementManager Instance;

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
        direction = PlayerDirection.Forward;
        playerInputManager = PlayerInputManager.Instance;
    }

    private void FixedUpdate()
    {
        PlayerForwardMovement();
        PlayerHorizontalMovement();
    }

    public void PlayerDirectionSetForward()
    { 
        HORIZONTAL_LIMIT_VALUE = 4;
        transform.DORotate(new Vector3(0, 0, 0), 0.1f);
        direction= PlayerDirection.Forward; 
    }
    private void PlayerForwardMovement()
    {
        transform.Translate(Vector3.forward * FORWARD_MOVEMENT_SPEED * Time.fixedDeltaTime);
    }

    public void PlayerHorizontalMovement()
    {
        if (playerInputManager.isActive)
        {
            if (direction == PlayerDirection.Left)
            {
                newPositionHorizontalValue = transform.position.z + playerInputManager.horizontalValue * HORIZONTAL_MOVEMENT_SPEED * Time.fixedDeltaTime;
                newPositionHorizontalValue = Mathf.Clamp(newPositionHorizontalValue, HORIZONTAL_LIMIT_VALUE - 8, HORIZONTAL_LIMIT_VALUE);
                transform.position = new Vector3(transform.position.x, transform.position.y, newPositionHorizontalValue);
            }
            else
            {
                newPositionHorizontalValue = transform.position.x + playerInputManager.horizontalValue * HORIZONTAL_MOVEMENT_SPEED * Time.fixedDeltaTime;
                newPositionHorizontalValue = Mathf.Clamp(newPositionHorizontalValue, HORIZONTAL_LIMIT_VALUE - 8, HORIZONTAL_LIMIT_VALUE);
                transform.position = new Vector3(newPositionHorizontalValue, transform.position.y, transform.position.z);
              
            }
        }

    } 

    public void RotatePlayer()
    {
        playerInputManager.ToggleIsActive();

        if (direction == PlayerDirection.Forward)
        {
            HORIZONTAL_LIMIT_VALUE = 362;
            transform.DORotate(new Vector3(0, -90, 0), ROTATION_ADNIMATION_TÝME).OnComplete(() =>
            {
                direction = PlayerDirection.Left;
                playerInputManager.ToggleIsActive();

            });

        }
        else if (direction == PlayerDirection.Left)
        {
            HORIZONTAL_LIMIT_VALUE = -354;
            transform.DORotate(new Vector3(0, 0, 0), ROTATION_ADNIMATION_TÝME).OnComplete(() =>
            {
                direction = PlayerDirection.Right;
                playerInputManager.ToggleIsActive();

            });

        }
        else if (direction == PlayerDirection.Right)
        {
            HORIZONTAL_LIMIT_VALUE = 4;
            transform.DORotate(new Vector3(0, 0, 0), ROTATION_ADNIMATION_TÝME).OnComplete(() =>
            {
                direction = PlayerDirection.Forward;
                playerInputManager.ToggleIsActive();

            });
        }
    }
    public void StopPlayer()
    {
        FORWARD_MOVEMENT_SPEED = 0f;
        HORIZONTAL_MOVEMENT_SPEED = 0f;
    }

    public void RunPlayer()
    {
        HORIZONTAL_MOVEMENT_SPEED = 10f;
        FORWARD_MOVEMENT_SPEED = 10f;
    }
}
