using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAt : MonoBehaviour
{
    private Vector3 worldPosition;
    private Vector3 screenPosition;
    public GameObject crosshair;
    // public Text nickNameText;

    private void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the game window
        // nickNameText.text = PhotonNetwork.LocalPlayer.NickName; 
    }

    void Update()
    {
        // Handle cursor visibility toggle
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the game window
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // Escape key pressed
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Release the cursor
        }

        // Update crosshair position
        UpdateCrosshairPosition();
    }

    void UpdateCrosshairPosition()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = 6f; // Make sure to set this according to your needs

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;

        crosshair.transform.position = Input.mousePosition;
    }
}
