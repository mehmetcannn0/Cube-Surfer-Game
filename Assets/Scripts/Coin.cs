using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    private void Start()
    {
        RotateCoin();
    }

    private void RotateCoin()
    { 
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);  
    }


    public void Collect()
    {

        Debug.Log("collected a coin");
        Destroy(gameObject);
    }
}
