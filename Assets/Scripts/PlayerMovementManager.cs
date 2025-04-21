using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private float newPositionX;
    private float HORIZONTAL_LIMIT_VALUE = 4;
    private float HORIZONTAL_MOVEMENT_SPEED =0;
    private float FORWARD_MOVEMENT_SPEED = 0;

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
    }

    private void FixedUpdate()
    {
        PlayerForwardMovement();
        PlayerHorizontalMovement();
    }

    private void PlayerForwardMovement()
    {
        transform.Translate(Vector3.forward * FORWARD_MOVEMENT_SPEED * Time.fixedDeltaTime);
    }

    public void PlayerHorizontalMovement()
    {
        newPositionX = transform.position.x + playerInputManager.horizontalValue * HORIZONTAL_MOVEMENT_SPEED * Time.fixedDeltaTime;
        //newPositionX = transform.position.x + playerInputManager.horizontalValue;
        newPositionX = Mathf.Clamp(newPositionX, -HORIZONTAL_LIMIT_VALUE, HORIZONTAL_LIMIT_VALUE);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        //transform.DOMoveX(newPositionX,1);

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
