using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab; // Assign your road piece prefab in the inspector
    public Transform player; // Assign your player's transform in the inspector
    public int initialRoadCount = 10; // Number of road pieces to spawn initially
    public float additionalSpacing = 5f; // Additional spacing between road pieces
    private float roadLength; // Length of a single road piece
    private float spawnX = 0f; // X position where the next road piece will be spawned
    private float safeZone = 500f; // Distance behind the player where road pieces start getting deleted
    private Queue<GameObject> activeRoads = new Queue<GameObject>();

    void Start()
    {
        roadLength = GetMaxChildLength(roadPrefab) + additionalSpacing;
        // Initialize spawnX to be directly next to the initial road piece
        spawnX = player.position.x - (roadLength * initialRoadCount / 100.0f);

        for (int i = 0; i < initialRoadCount; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        // Check if it's time to spawn a new road piece based on the player's X position
        if (player.position.x + safeZone > (spawnX + roadLength * activeRoads.Count))
        {
            SpawnRoad();
            DeleteRoad();
        }
    }

    private void SpawnRoad()
    {
        // Calculate the X position for the new road piece and increment spawnX for the next piece
        GameObject go = Instantiate(roadPrefab, new Vector3(spawnX, 0, 0), Quaternion.identity);
        activeRoads.Enqueue(go);
        spawnX += roadLength; // Move spawnX to the right for the next road piece
    }

    private void DeleteRoad()
    {
        GameObject oldRoad = activeRoads.Dequeue();
        Destroy(oldRoad);
    }

    private float GetMaxChildLength(GameObject prefab)
    {
        float maxLength = 0f;
        Renderer[] childRenderers = prefab.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in childRenderers)
        {
            if (rend.bounds.size.x > maxLength) // Adjusting to check along the X-axis
            {
                maxLength = rend.bounds.size.x;
            }
        }
        return maxLength;
    }
}
