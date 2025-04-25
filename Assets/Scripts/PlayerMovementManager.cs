using DG.Tweening;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] float forwardMovementSpeed = 0;

    private float horizontalLimitValue = 4;
    private float horizontalMovementSpeed = 0;
    private const float ROTATION_ANIMATION_TÝME = 1f;

    private float newPositionHorizontalValue;

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
        playerInputManager = PlayerInputManager.Instance;
        direction = PlayerDirection.Forward;
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelRestarted += PlayerDirectionSetForward;
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelRestarted -= PlayerDirectionSetForward;        
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
        direction= PlayerDirection.Forward; 
    }
    private void PlayerForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardMovementSpeed * Time.fixedDeltaTime);
    }

    public void PlayerHorizontalMovement()
    {
        if (playerInputManager.isActive)
        {
            if (direction == PlayerDirection.Left)
            {
                newPositionHorizontalValue = transform.position.z + playerInputManager.horizontalValue * horizontalMovementSpeed * Time.fixedDeltaTime;
                newPositionHorizontalValue = Mathf.Clamp(newPositionHorizontalValue, horizontalLimitValue - 8, horizontalLimitValue);
                transform.position = new Vector3(transform.position.x, transform.position.y, newPositionHorizontalValue);
            }
            else
            {
                newPositionHorizontalValue = transform.position.x + playerInputManager.horizontalValue * horizontalMovementSpeed * Time.fixedDeltaTime;
                newPositionHorizontalValue = Mathf.Clamp(newPositionHorizontalValue, horizontalLimitValue - 8, horizontalLimitValue);
                transform.position = new Vector3(newPositionHorizontalValue, transform.position.y, transform.position.z);
              
            }
        } 
    } 

    public void RotatePlayer()
    {
        playerInputManager.ToggleIsActive();

        if (direction == PlayerDirection.Forward)
        {
            horizontalLimitValue = 362;
            transform.DORotate(new Vector3(0, -90, 0), ROTATION_ANIMATION_TÝME).OnComplete(() =>
            {
                direction = PlayerDirection.Left;
                playerInputManager.ToggleIsActive();

            });

        }
        else if (direction == PlayerDirection.Left)
        {
            horizontalLimitValue = -354;
            transform.DORotate(new Vector3(0, 0, 0), ROTATION_ANIMATION_TÝME).OnComplete(() =>
            {
                direction = PlayerDirection.Right;
                playerInputManager.ToggleIsActive();

            });

        }
        else if (direction == PlayerDirection.Right)
        {
            horizontalLimitValue = 4;
            transform.DORotate(new Vector3(0, 0, 0), ROTATION_ANIMATION_TÝME).OnComplete(() =>
            {
                direction = PlayerDirection.Forward;
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
