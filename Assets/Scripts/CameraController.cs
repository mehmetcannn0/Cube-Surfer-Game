using UnityEngine;
  
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cubeParent;
    [SerializeField] Transform lookTarget;

    void LateUpdate()
    {
        int cubeCount = cubeParent.childCount;

        float distance = Mathf.Max(Utils.BASE_DISTANCE + cubeCount * Utils.DISTANCE_PER_CUBE, Utils.MIN_DISTANCE);
        float height = Mathf.Min(Utils.BASE_HEIGHT + cubeCount * Utils.HEIGHT_PER_CUBE, Utils.MAX_HEIGHT);

        Vector3 targetPosition = cubeParent.position - cubeParent.forward * distance + Vector3.up * height;

        transform.position = Vector3.Lerp(
           transform.position,
           targetPosition,
           Time.deltaTime * Utils.CAMERA_FOLLOW_SPEED
       );

        transform.LookAt(lookTarget.position + Vector3.up * 1.5f);
    }
}
