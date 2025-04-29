using DG.Tweening;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private float forwardMovementSpeed = 0;
    private float horizontalLimitValue = 4;
    private float horizontalMovementSpeed = 0;
    private float newPositionHorizontalValue;

    public PlayerDirection Direction;

    PlayerInputManager playerInputManager;

    public static PlayerMovementManager Instance;

    public enum PlayerDirection
    {
        Forward,
        Left,
        Right
    }

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
        playerInputManager = PlayerInputManager.Instance;
        Direction = PlayerDirection.Forward;
    }

    private void OnEnable()
    {
        ActionController.OnLevelRestarted += PlayerDirectionSetForward;
        ActionController.OnNextLevelStarted += PlayerDirectionSetForward;
    }

    private void OnDisable()
    {
        ActionController.OnLevelRestarted -= PlayerDirectionSetForward;
        ActionController.OnNextLevelStarted -= PlayerDirectionSetForward;
    }

    private void FixedUpdate()
    {
        PlayerForwardMovement();
        PlayerHorizontalMovement();
    }

    public void PlayerDirectionSetForward()
    {
        horizontalLimitValue = 4;
        transform.DORotate(new Vector3(0, 0, 0), 0.1f);
        Direction = PlayerDirection.Forward;
    }
    private void PlayerForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardMovementSpeed * Time.fixedDeltaTime);
    }

    public void PlayerHorizontalMovement()
    {
        if (playerInputManager.IsActive)
        {
            if (Direction == PlayerDirection.Left)
            {
                newPositionHorizontalValue = transform.position.z + playerInputManager.HorizontalValue * horizontalMovementSpeed * Time.fixedDeltaTime;
                newPositionHorizontalValue = Mathf.Clamp(newPositionHorizontalValue, horizontalLimitValue - 8, horizontalLimitValue);
                transform.position = new Vector3(transform.position.x, transform.position.y, newPositionHorizontalValue);
            }
            else
            {
                newPositionHorizontalValue = transform.position.x + playerInputManager.HorizontalValue * horizontalMovementSpeed * Time.fixedDeltaTime;
                newPositionHorizontalValue = Mathf.Clamp(newPositionHorizontalValue, horizontalLimitValue - 8, horizontalLimitValue);
                transform.position = new Vector3(newPositionHorizontalValue, transform.position.y, transform.position.z);

            }
        }
    }

    public void RotatePlayer()
    {
        playerInputManager.ToggleIsActive();

        if (Direction == PlayerDirection.Forward)
        {
            horizontalLimitValue = 362;
            transform.DORotate(new Vector3(0, -90, 0), Utils.ROTATION_ANIMATION_TÝME).OnComplete(() =>
            {
                Direction = PlayerDirection.Left;
                playerInputManager.ToggleIsActive();

            });

        }
        else if (Direction == PlayerDirection.Left)
        {
            horizontalLimitValue = -354;
            transform.DORotate(new Vector3(0, 0, 0), Utils.ROTATION_ANIMATION_TÝME).OnComplete(() =>
            {
                Direction = PlayerDirection.Right;
                playerInputManager.ToggleIsActive();

            });

        }
        else if (Direction == PlayerDirection.Right)
        {
            horizontalLimitValue = 4;
            transform.DORotate(new Vector3(0, 0, 0), Utils.ROTATION_ANIMATION_TÝME).OnComplete(() =>
            {
                Direction = PlayerDirection.Forward;
                playerInputManager.ToggleIsActive();

            });
        }
    }

    public void StopPlayer()
    {
        forwardMovementSpeed = 0f;
        horizontalMovementSpeed = 0f;
    }

    public void RunPlayer()
    {
        horizontalMovementSpeed = 10f;
        forwardMovementSpeed = 10f;
    }
}
