using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign in the inspector
    public GameObject aimRef; // Assign this in the inspector

    // Update is called once per frame
    void Update()
    {
        // Toggle pause state when ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume normal time
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
        aimRef.SetActive(true); // Reactivate AimRef when resuming
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze time
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
        aimRef.SetActive(false); // Deactivate AimRef when pausing
    }

    public void LoadStartMenu()
    {
        Time.timeScale = 1f; // Ensure time is resumed
        SceneManager.LoadScene("StartMenu"); // Replace "StartMenu" with the name of your start menu scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
