using UnityEngine;

public class Cube : MonoBehaviour, ICollectable
{
    private BoxCollider boxCollider;
    //private Transform cubeParents;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

    }

    public void OnCollect(Transform cubesParentsTransform, Vector3 lowerCubePosition)
    {
        Debug.Log("collectable");
        //transform.parent = cubeParents;
        transform.parent = cubesParentsTransform;
        transform.localPosition = lowerCubePosition;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("trigger");
    //    if (other.TryGetComponent(out ICollectable collectable))
    //    {
    //        collectable.OnCollect();
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("collision");
    //    if (collision.gameObject.TryGetComponent(out IObstacle obstacle))
    //    {
    //        obstacle.OnHit();
    //    }
    //}


}
