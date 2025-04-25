using UnityEngine;

public class WallTower : MonoBehaviour,IObstacle
{
  public  int wallSize;
    public int OnHit()
    {
        return wallSize;

    }

}
