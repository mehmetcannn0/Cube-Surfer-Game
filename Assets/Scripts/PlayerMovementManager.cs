using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] float forwardMovementSpeed;
    [SerializeField] float horizontalMovementSpeed;
    [SerializeField] float horizontalLimitValue;

    private float newPositionX; 

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


    public void PlayerHorizontalMovement()
    {
        newPositionX = transform.position.x + playerInputManager.horizontalValue * horizontalMovementSpeed * Time.fixedDeltaTime;
        //newPositionX = transform.position.x + playerInputManager.horizontalValue;
        newPositionX = Mathf.Clamp(newPositionX, -horizontalLimitValue, horizontalLimitValue);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        //transform.DOMoveX(newPositionX,1);

    }
}
