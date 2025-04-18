using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable 
{
    int OnCollect( Transform cubesParentsTransform,Vector3 upperCubePosition ,int cubeIndex);

}
