using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] float forwardMovementSpeed;
    [SerializeField] float horizontalMovementSpeed;
    [SerializeField] float horizontalLimitValue;

    private float newPositionX;
    //karakter hareketi
    // hareket l�m�tlenecek clamp �le   x 4,-4 aras�nda olacak

   public static  PlayerMovementManager Instance;
    PlayerInputManager playerInputManager;

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
        transform.Translate(Vector3.forward * forwardMovementSpeed * Time.fixedDeltaTime);

    }


    private void PlayerHorizontalMovement()
    {
        newPositionX = transform.position.x + playerInputManager.horizontalValue * horizontalMovementSpeed * Time.fixedDeltaTime;
        newPositionX = Mathf.Clamp(newPositionX, -horizontalLimitValue, horizontalLimitValue);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }
}
