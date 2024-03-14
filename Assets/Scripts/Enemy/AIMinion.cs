using UnityEngine;
using UnityEngine.AI;

public class AIMinion : MonoBehaviour
{
    GameObject playerTarget; // Target for the minion to chase
    public float attackDistance = 100f; // Distance at which the minion will attack
    public float moveSpeed = 3.5f; // Movement speed of the minion

    public NavMeshAgent navAgent;
    public float distanceToPlayer;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (!playerTarget)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player");
        }
        navAgent.speed = moveSpeed;
    }

    void Update()
    {
        if (!playerTarget) return;

        distanceToPlayer = Vector3.Distance(playerTarget.transform.position, transform.position);

        if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    
    private void MoveTowardsPlayer()
    {
        if (navAgent != null && navAgent.isOnNavMesh)
        {
            Vector3 targetDirection = (playerTarget.transform.position - transform.position).normalized;
            // Negate the target direction to face the opposite way
            targetDirection = -targetDirection;
            // Optionally, you can ignore the y component to keep the rotation horizontal
            targetDirection.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 500f); // Adjust the rotation speed as needed

            navAgent.SetDestination(playerTarget.transform.position);
        }
    }

    private void AttackPlayer()
    {
        // Here you can implement logic for attacking the player
        // For example, triggering an event, playing a sound effect, etc.
        Debug.Log("Attacking the player.");
        // Ensure the minion stops moving when attacking
        navAgent.isStopped = true;
    }
}
