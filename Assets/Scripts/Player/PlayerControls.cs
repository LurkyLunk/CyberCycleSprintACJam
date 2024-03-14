using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Laser gun array")]
    [Tooltip("Add all player laser here")]
    [SerializeField] GameObject[] lasers;

    [Header("Laser Movement")]
    [Tooltip("Speed at which the lasers move to match crosshair position")]
    [SerializeField] float laserMoveSpeed = 10f;
    [Tooltip("Maximum distance in front of the player where the lasers aim")]
    [SerializeField] float aimDistance = 100f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Cache the main camera
    }

    void Update()
    {
        ProcessFiring();
        ProcessLaserMovement();
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    void ProcessLaserMovement()
    {
        Vector3 aimPoint = GetAimPoint();
        foreach (GameObject laser in lasers)
        {
            // Assuming the lasers should move towards the aimPoint
            Vector3 direction = (aimPoint - laser.transform.position).normalized;
            // Move each laser towards the aimPoint, but keep them at their current height
            laser.transform.position += direction * laserMoveSpeed * Time.deltaTime;
        }
    }

    Vector3 GetAimPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(-mainCamera.transform.forward, transform.position + mainCamera.transform.forward * aimDistance);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position + transform.forward * aimDistance;
    }
}
