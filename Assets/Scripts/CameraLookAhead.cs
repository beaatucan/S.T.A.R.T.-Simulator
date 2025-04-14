using UnityEngine;
using Cinemachine;

public class CameraLookAhead : MonoBehaviour
{
    public Transform player; // Assign your player Transform
    public CinemachineVirtualCamera virtualCamera;
    public float lookAheadDistance = 2f; // Distance to look ahead in the movement direction

    private Vector3 previousPosition;

    private void Start()
    {
        if (player == null || virtualCamera == null)
        {
            Debug.LogError("Player or Virtual Camera is not assigned!");
            enabled = false;
        }

        previousPosition = player.position;
    }

    private void LateUpdate()
    {
        // Calculate movement direction
        Vector3 movementDirection = (player.position - previousPosition).normalized;

        // Apply look-ahead offset
        CinemachineFramingTransposer framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (framingTransposer != null)
        {
            framingTransposer.m_TrackedObjectOffset = new Vector3(movementDirection.x * lookAheadDistance, movementDirection.y * lookAheadDistance, 0);
        }

        // Update previous position
        previousPosition = player.position;
    }
}
