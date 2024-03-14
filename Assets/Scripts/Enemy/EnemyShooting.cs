using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform barrelEnd; // Assign the barrel end GameObject in the Inspector
    public GameObject projectilePrefab; // Assign your projectile prefab in the Inspector
    public float fireRate = 1f; // How often the enemy fires
    private float nextFireTime = 1f; // Internal timer to track firing rate
    public float maxDistance = 100f; // Max distance to check for line of sight and firing

    private GameObject playerTarget; // To store the player GameObject

    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (playerTarget == null) return;

        float distanceToPlayer = Vector3.Distance(playerTarget.transform.position, transform.position);

        if (distanceToPlayer <= maxDistance)
        {
            // Optionally check for line of sight
            if (HasLineOfSight())
            {
                if (Time.time >= nextFireTime)
                {
                    FireProjectile();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }
    }

    bool HasLineOfSight()
    {
        Vector3 direction = playerTarget.transform.position - barrelEnd.position;
        RaycastHit hit;

        if (Physics.Raycast(barrelEnd.position, direction.normalized, out hit, maxDistance))
        {
            Debug.DrawLine(barrelEnd.position, hit.point, Color.red); // For visual debugging
            if (hit.collider.gameObject == playerTarget)
            {
                Debug.Log("Player in sight, hit: " + hit.collider.gameObject.name);
                return true;
            }
            else
            {
                Debug.Log("Line of sight blocked by: " + hit.collider.gameObject.name);
            }
        }
        return false;
    }

    void FireProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is null.");
            return;
        }

        Debug.Log("Firing projectile at " + Time.time);
        GameObject projectile = Instantiate(projectilePrefab, barrelEnd.position, barrelEnd.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = (playerTarget.transform.position - barrelEnd.position).normalized * 200f; // Adjust the speed as necessary
        }
        else
        {
            Debug.LogError("Projectile prefab does not have a Rigidbody component.");
        }
    }
}
