using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour,IObstacle
{
    public void OnHit()
    {
        Debug.Log("obstacle");

    }

}
