using UnityEngine;

public class Wall : MonoBehaviour,IObstacle
{
  public  int wallSize; 

    public int OnHit()
    {
        return wallSize;

    }

}
