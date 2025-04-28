using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cubeParent;
    [SerializeField] Transform lookAtTarget;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private const float BASE_LOOK_AT_Y = 3f;
    private const float Y_PER_CUBE = 0.6f;
    private const float MAX_LOOK_AT_Y = 35f;

    private const float BASE_CAMERA_Z = -16f;
    private const float Z_PER_CUBE = -0.8f;
    private const float MIN_CAMERA_Z = -55f;

    private void LateUpdate()
    {
        int cubeCount = cubeParent.childCount;

        float newY = BASE_LOOK_AT_Y + (cubeCount * Y_PER_CUBE);
        newY = Mathf.Min(newY, MAX_LOOK_AT_Y);
        Vector3 lookAtPos = lookAtTarget.localPosition;
        lookAtPos.y = newY;
        lookAtTarget.localPosition = Vector3.Lerp(lookAtTarget.localPosition, lookAtPos, Time.deltaTime * 10f);

        var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 offset = transposer.m_FollowOffset;
        float newZ = BASE_CAMERA_Z + (cubeCount * Z_PER_CUBE);
        newZ = Mathf.Max(newZ, MIN_CAMERA_Z);
        offset.z = newZ;
        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, offset, Time.deltaTime * 10f);
    }

}
