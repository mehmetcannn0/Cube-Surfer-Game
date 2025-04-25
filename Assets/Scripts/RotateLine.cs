using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLine : MonoBehaviour, IRotate
{
    public void RotatePlayer()
    {
        PlayerMovementManager.Instance.RotatePlayer();
    }
}
