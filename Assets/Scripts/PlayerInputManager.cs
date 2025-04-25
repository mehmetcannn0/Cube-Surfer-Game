using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public float horizontalValue { get; private set; }

    public static PlayerInputManager Instance;
    public bool isActive { get; private set; }
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
        ToggleIsActive();
    }

    void Update()
    {
        HandleHeroHorizontalInput();
    }

    private void HandleHeroHorizontalInput()
    {
        if (Input.touchCount > 0 && isActive)
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

    public void ToggleIsActive()
    {
        isActive = !isActive;
        //Debug.Log("isActive " + isActive);
    }
}

