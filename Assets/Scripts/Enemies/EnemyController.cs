using UnityEngine;

// Enemy controller logic
// Enemies will attack a player if they enter the enemy's detection range
// The enemy will move towards the player and shoot at the player
// The enemy will stop attacking if the player leaves the extended detection range (larger than inital range)
public class EnemyController : MonoBehaviour
{
    [Header("Attack Settings")]
    public float detectionRange = 10f;
    public float extendedDetectionRange = 15f;
    public float moveSpeed = 3f;
    public float fireRate = 1f;
    public Transform firePoint;
    public string projectilePoolTag = "EnemyProjectile";

    private Transform player;
    private bool isAttacking = false;
    private float timeSinceLastShot;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Determine if the enemy should be in the attacking state
        if (!isAttacking && distanceToPlayer < detectionRange)
        {
            isAttacking = true;
        }
        else if (isAttacking && distanceToPlayer > extendedDetectionRange)
        {
            isAttacking = false;
        }

        if (isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Aim at the player
        transform.LookAt(player);

        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        // Handle shooting
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= 1f / fireRate)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    private void Shoot()
    {
    }
}
