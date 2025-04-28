using System.Collections.Generic;
using UnityEngine;

public class CubeTower : MonoBehaviour, IStackable
{
    private BoxCollider cubeTowerBoxCollider;
    private const int CUBE_WIDTH = 2;

    public List<GameObject> CubeList;
    public int CubeSize;

    PlayerInteractionController playerInteractionController;

    private void Start()
    {
        cubeTowerBoxCollider = GetComponent<BoxCollider>();
        playerInteractionController = PlayerInteractionController.Instance;
    }

    public int OnStack()
    {
        cubeTowerBoxCollider.enabled = false;
        for (int i = 0; i < CubeSize; i++)
        {
            CubeList[i].transform.SetParent(playerInteractionController.CubeParent);
            CubeList[i].transform.localPosition = playerInteractionController.PlayerVisualTransform.localPosition + Vector3.up * CUBE_WIDTH * i;
        }
        return CubeSize;
    }
}
