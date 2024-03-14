using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI elements manipulation

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private ParticleSystem crashVFX;
    [SerializeField] private GameObject[] projectileHitIndicators; // Assign in inspector
    [SerializeField] private GameObject[] enemyCollisionIndicators; // Assign in inspector
    [SerializeField] private ParticleSystem hitVFX; // Assign in inspector
    [SerializeField] private RectTransform playerUIImage; // Assign the RectTransform of the player character's UI image in the inspector

    private int enemyCollisionCount = 0; // Tracks collisions with enemies
    private int projectileHitCount = 0; // Tracks hits from enemy projectiles
    private bool hasCrashed = false;

    private void Start()
    {
        InitializeIndicators();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCrashed) return; // Prevents further collision handling after crash

        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyCollisionCount++;
            UpdateIndicators(enemyCollisionIndicators, enemyCollisionCount);
            PlayHitVFX(collision.contacts[0].point); // Play the hit VFX
            StartCoroutine(ShakePlayerImage()); // Shake player UI image

            if (enemyCollisionCount >= 5)
            {
                StartCrashSequence();
            }
        }
        else if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            projectileHitCount++;
            UpdateIndicators(projectileHitIndicators, projectileHitCount);
            PlayHitVFX(collision.contacts[0].point); // Play the hit VFX
            StartCoroutine(ShakePlayerImage()); // Shake player UI image

            if (projectileHitCount >= 10)
            {
                StartCrashSequence();
            }
        }
    }

    private void PlayHitVFX(Vector3 position)
    {
        if (hitVFX != null)
        {
            hitVFX.transform.position = position;
            hitVFX.Play();
        }
    }

    private void InitializeIndicators()
    {
        UpdateIndicators(projectileHitIndicators, 0);
        UpdateIndicators(enemyCollisionIndicators, 0);
    }

    private void UpdateIndicators(GameObject[] indicators, int hitCount)
    {
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].SetActive(i < indicators.Length - hitCount);
        }
    }

    private void StartCrashSequence()
    {
        hasCrashed = true;
        crashVFX.Play();

        // Optionally, disable player mesh renderers or controls here
        // GetComponent<PlayerControls>().enabled = false; // Example for disabling player controls

        StartCoroutine(ReloadLevelWithDelay());
    }

    IEnumerator ReloadLevelWithDelay()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ShakePlayerImage(float duration = 0.5f, float magnitude = 5f)
    {
        Vector3 originalPosition = playerUIImage.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;

            playerUIImage.localPosition = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;

            yield return null; // Wait until the next frame
        }

        playerUIImage.localPosition = originalPosition; // Reset to the original position
    }

    public void ResetIndicators()
    {
        enemyCollisionCount = 0;
        projectileHitCount = 0;
        UpdateIndicators(projectileHitIndicators, projectileHitCount);
        UpdateIndicators(enemyCollisionIndicators, enemyCollisionCount);
    }
}
