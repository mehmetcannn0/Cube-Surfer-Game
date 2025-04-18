using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IStackable
{
    private BoxCollider boxCollider; 
    public int cubeSize;
    public List<Cube> cubeList;
    PlayerInteractionController playerInteractionController;

    private void Start()
    {
        playerInteractionController = PlayerInteractionController.Instance;

        boxCollider = GetComponent<BoxCollider>();
        cubeList.Add(this);
    }

    public int OnStack(  int cubeIndex)
    { 
        cubeList[cubeIndex].transform.parent = playerInteractionController. cubeParent;
        cubeList[cubeIndex].boxCollider.enabled = false;
        cubeList[cubeIndex].transform.localPosition = playerInteractionController. playerVisualTransform.localPosition;
        return cubeSize;
    }
     
}
