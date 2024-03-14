using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform muzzleSpawn;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private float maxDistance = 100f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the muzzle flash effect
        if (muzzleFlashPrefab)
        {
            var flashInstance = Instantiate(muzzleFlashPrefab, muzzleSpawn.position, muzzleSpawn.rotation, muzzleSpawn);
            Destroy(flashInstance, 0.1f); // Destroy the muzzle flash instance after a short duration
        }

        // Play the shooting sound effect
        if (shotSound && audioSource)
        {
            audioSource.PlayOneShot(shotSound);
        }

        // Perform the raycast from the muzzle point forward
        RaycastHit hit;
        if (Physics.Raycast(muzzleSpawn.position, muzzleSpawn.forward, out hit, maxDistance, hitLayers))
        {
            Debug.Log("Hit: " + hit.transform.name); // Log the name of the hit object

            // Check if the hit object has an Enemies component and call TakeDamage on it
            Enemies enemy = hit.transform.GetComponent<Enemies>();
            if (enemy != null)
            {
                enemy.TakeDamage();
            }
        }
    }
}
