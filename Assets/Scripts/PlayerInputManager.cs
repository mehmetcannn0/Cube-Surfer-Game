using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public float horizontalValue { get; private set; }

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

    void Update()
    {
        HandleHeroHorizontalInput();
    }

    private void HandleHeroHorizontalInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                horizontalValue = touch.deltaPosition.x * 15f * Time.deltaTime;
               // horizontalValue = (touch.deltaPosition.x < 0 ? -2f : 2f) * Time.deltaTime;
            }
        }
        else
        {
            horizontalValue = 0;
        }
    }
}

  //switch (touch.phase)
  //          {
  //              case TouchPhase.Moved:
  //                  input = touch.deltaPosition.x < 0 ? -2f : 2f;
  //                  break;
  //              case TouchPhase.Ended:
  //                  horizontalValue = input;
  //                  break;

  //          }   