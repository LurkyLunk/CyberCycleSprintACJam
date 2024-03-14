using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro elements

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeSurvivedText; // Assign in the inspector
    public TextMeshProUGUI distanceTraveledText; // Assign in the inspector
    public Transform player; // Assign your player's transform in the inspector

    private float startTime;
    private Vector3 startPosition;
    private float distanceTraveled;

    void Start()
    {
        // Initialize startTime to the current time
        startTime = Time.time;

        // Initialize startPosition to the player's starting position
        startPosition = player.position;
    }

    void Update()
    {
        // Calculate the elapsed time in seconds and update the timeSurvivedText
        float timeSurvived = Time.time - startTime;
        timeSurvivedText.text = FormatTime(timeSurvived);

        // Calculate the distance traveled in feet and update the distanceTraveledText
        distanceTraveled = Vector3.Distance(startPosition, player.position);
        distanceTraveledText.text = $"{distanceTraveled * 3.28084f:F2} feet"; // Convert meters to feet
    }

    // Helper method to format the time survived into a minutes:seconds format
    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60F);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60F);
        return $"{minutes:00}:{seconds:00}";
    }
}
