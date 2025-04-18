using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public float horizontalValue { get; private set; }

    //PlayerMovementManager playerMovementManager;


    public static PlayerInputManager Instance;

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
    //private void Start()
    //{
    //    playerMovementManager = PlayerMovementManager.Instance;
    //}
    void Update()
    {
        HandleHeroHorizontalInput();
    }

    private void HandleHeroHorizontalInput()
    {
         /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    float deltaX = touch.deltaPosition.x;

                    if (deltaX > 10f) // saða kaydýrma
                    {
                        horizontalValue = 2;
                    }
                    else if (deltaX < -10f) // sola kaydýrma
                    {
                        horizontalValue = -2;
                    }
                    break;

                case TouchPhase.Ended:
                    playerMovementManager.PlayerHorizontalMovement();
                    break;
            }
        }*/
       
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                horizontalValue = touch.deltaPosition.x * 15f * Time.deltaTime;
            }
        }
        else
        {
            horizontalValue = 0;
        }
    }
    }

  