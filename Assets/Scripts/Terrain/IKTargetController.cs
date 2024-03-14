using UnityEngine;

public class IKTargetController : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public float distanceFromCamera = 1f; // Distance in front of the camera to place the IK target

    void Update()
    {
        MoveIKTargetToCrosshair();
    }

    void MoveIKTargetToCrosshair()
    {
        Vector3 screenPosition = Input.mousePosition; // Get the current mouse position
        // Convert the screen position of the crosshairs to a world point
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
        transform.position = worldPosition; // Move the IK target to this world position
    }
}
