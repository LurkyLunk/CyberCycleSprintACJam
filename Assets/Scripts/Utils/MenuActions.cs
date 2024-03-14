using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class MenuActions : MonoBehaviour
{
    public void StartGame()
    {
        // Assuming "MainScene" is the name of your main game scene
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
