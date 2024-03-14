using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform[] spawnPoints; // An array of transforms from where enemies can be spawned
    public float spawnDelay = 5f; // Delay in seconds before respawning enemies
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // Keeps track of spawned enemies

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemiesStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemiesStart()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedEnemies.Add(spawnedEnemy);
        }

        // Optionally, start a coroutine to respawn enemies
        StartCoroutine(RespawnEnemiesCoroutine());
    }

    IEnumerator RespawnEnemiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            // Check and respawn enemies if they were destroyed
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                if (spawnedEnemies[i] == null)
                {
                    spawnedEnemies[i] = Instantiate(enemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
                }
            }
        }
    }
}
