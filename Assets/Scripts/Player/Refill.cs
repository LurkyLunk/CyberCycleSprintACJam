using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Refill : MonoBehaviour
{
    public float respawnTime = 5f; // Time it takes for the refill object to become available again after being used
    private AudioSource audioSource; // AudioSource component for playing sounds
    public AudioClip refillSound; // Sound clip to play when the refill is used

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RefillAmmoAndResetIndicators(other.gameObject);
            PlayRefillSound();
            StartCoroutine(DisableAndRespawn());
        }
    }

    private void RefillAmmoAndResetIndicators(GameObject player)
    {
        // Refill Ammo
        ProjectileShoot ammoManager = player.GetComponent<ProjectileShoot>();
        if (ammoManager != null)
        {
            ammoManager.RefillAmmo(); // Ensure AmmoManager has a method to refill ammo
        }

        // Reset Collision and Projectile Hit Indicators
        CollisionHandler healthManager = player.GetComponent<CollisionHandler>();
        if (healthManager != null)
        {
            healthManager.ResetIndicators(); // Ensure HealthManager has a method to reset indicators
        }
    }

    private void PlayRefillSound()
    {
        if (refillSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(refillSound);
        }
    }

    IEnumerator DisableAndRespawn()
    {
        // Optionally disable the refill object visually and/or physically
        SetActiveState(false);

        yield return new WaitForSeconds(respawnTime);

        // Re-enable the refill object for future use
        SetActiveState(true);
    }

    private void SetActiveState(bool state)
    {
        // Example of disabling/enabling the GameObject or components,
        // adjust based on how you want the respawn behavior to work
        GetComponent<Renderer>().enabled = state;
        GetComponent<Collider>().enabled = state;

        // If the refill object has child objects you want to disable/enable,
        // iterate through them and set their active state as well
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}
