using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour,IObstacle
{
  public  int wallSize; 

    private void Start()
    { 
        //wallSize = 2;
    }
    public int OnHit()
    {
        return wallSize;

    }

}
