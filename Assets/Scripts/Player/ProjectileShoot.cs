using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProjectileShoot : MonoBehaviour
{
    public Transform shootPoint; // The point from which the raycast originates, if needed
    public Transform muzzleSpawn; // The point from which the muzzle flash originates
    public Camera mainCamera; // Reference to the main camera
    public LayerMask hitLayers; // Layers that the raycast can hit
    public float maxDistance = 1000f; // Maximum distance the raycast will check for hits
    public GameObject hitEffectPrefab; // Prefab to instantiate at the hit location
    public GameObject muzzleFlashPrefab; // Prefab for the muzzle flash effect
    public AudioClip gunSound; // Sound clip to play when firing
    public float fireRate = 10f; // Bullets per second
    public int maxAmmo = 30; // Maximum ammo
    public TextMeshProUGUI ammoTextUI;

    private AudioSource audioSource; // AudioSource component for playing sounds
    private float nextTimeToFire = 0f; // When the next shot can be fired
    private int currentAmmo; // Current ammo

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        currentAmmo = maxAmmo; // Initialize ammo
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            ShootRaycast();
            PlayGunSound(); // Play the gun sound
            PlayMuzzleFlash(); // Play the muzzle flash effect
            currentAmmo--; // Decrement ammo
            UpdateAmmoUI();
        }
    }
   
    void ShootRaycast()
    {
        Vector3 screenPosition = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, hitLayers))
        {
            Debug.Log("Hit: " + hit.transform.name);

            if (hitEffectPrefab != null)
            {
                // Correctly declare the hitEffectInstance variable here
                GameObject hitEffectInstance = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitEffectInstance, 2f); // Now correctly referring to the declared variable
            }

            // Assuming you have an Enemies script attached to your enemy GameObjects
            // and it has a method called TakeDamage for handling damage
            Enemies enemy = hit.transform.GetComponent<Enemies>();
            if (enemy != null)
            {
                enemy.TakeDamage(); // Call the TakeDamage method on the enemy
            }
        }
    }

    void PlayGunSound()
    {
        if (gunSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gunSound);
        }
    }

    void PlayMuzzleFlash()
    {
        if (muzzleFlashPrefab != null && muzzleSpawn != null)
        {
            var muzzleFlashInstance = Instantiate(muzzleFlashPrefab, muzzleSpawn.position, muzzleSpawn.rotation, muzzleSpawn);
            Destroy(muzzleFlashInstance, 0.1f); // Muzzle flash is usually very quick
        }
    }

    void UpdateAmmoUI()
    {
        ammoTextUI.text = "Ammo: " + currentAmmo.ToString();
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo; // Reset ammo to max
        UpdateAmmoUI();
    }
}