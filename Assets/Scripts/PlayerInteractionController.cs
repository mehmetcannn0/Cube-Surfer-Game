using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Transform cubeParent;
    [SerializeField] private Transform playerV�sualTransform;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        Vector3 playerVisualV3 = playerV�sualTransform.localPosition;
        if (other.TryGetComponent(out ICollectable collectable))
        {
            collectable.OnCollect(cubesParentsTransform: cubeParent, lowerCubePosition: playerVisualV3);
            playerV�sualTransform.localPosition = new Vector3(playerVisualV3.x, playerVisualV3.y + 2, playerVisualV3.z);
            return;
        }
        if (other.TryGetComponent(out IObstacle obstacle))
        {
            obstacle.OnHit();
            return;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PlayerInteractionController collision");
    }
}
