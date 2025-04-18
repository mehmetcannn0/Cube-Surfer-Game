using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, ICollectable
{
    private BoxCollider boxCollider; 
    public int cubeSize;
    public List<Cube> cubeList;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        cubeList.Add(this); 
    }

    public int OnCollect(Transform cubesParentsTransform, Vector3 upperCubePosition, int cubeIndex)
    { 
        cubeList[cubeIndex].transform.parent = cubesParentsTransform;
        cubeList[cubeIndex].boxCollider.enabled = false;
        cubeList[cubeIndex].transform.localPosition = upperCubePosition;
        return cubeSize;
    }
     
}
