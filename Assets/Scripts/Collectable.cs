using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable 
{
    void OnCollect( Transform cubesParentsTransform,Vector3 lowerCubePosition);
}
