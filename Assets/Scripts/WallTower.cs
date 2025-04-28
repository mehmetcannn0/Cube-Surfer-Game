using UnityEngine;

public class WallTower : MonoBehaviour, IObstacle
{
    public int WallSize;
    public int OnHit()
    {
        return WallSize;

    }

}
