using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cubeParent;

    [SerializeField] Transform lookAtTarget;
    private float baseLookAtY = 3f;
    private float yPerCube = 0.6f;
    private float maxLookAtY = 35f;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private float baseCameraZ = -16f;
    private float zPerCube = -0.8f;
    private float minCameraZ = -55f;



    private void LateUpdate()
    {
        int cubeCount = cubeParent.childCount;

        float newY = baseLookAtY + (cubeCount * yPerCube);
        newY = Mathf.Min(newY, maxLookAtY);
        Vector3 lookAtPos = lookAtTarget.localPosition;
        lookAtPos.y = newY;
        lookAtTarget.localPosition = Vector3.Lerp(lookAtTarget.localPosition, lookAtPos, Time.deltaTime * 10f);

        var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        Vector3 offset = transposer.m_FollowOffset;
        float newZ = baseCameraZ + (cubeCount * zPerCube);
        newZ = Mathf.Max(newZ, minCameraZ);
        offset.z = newZ;
        transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, offset, Time.deltaTime * 10f);
    }

}
