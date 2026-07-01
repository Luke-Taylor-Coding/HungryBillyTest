using UnityEngine;
using UnityEngine.AI;

// A controller script for the basic enemy behavior
// Current basic behavior involves wandering and attacking / chasing the player when in rage
// Uses unity NavMeshAgent for pathfinding and movement

// Player detection logic
// Enemy has a FOV range, this is a cone detection script that checks for the player tag
// On finding a player tag it will raycast to check if the player is in line of sight
// If so it will set the player as the target and start chasing / attacking

enum EnemyState
{
    Idle,
    Attacking
}

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Wander Settings")]
    [SerializeField] private float m_wanderRadius = 5f;
    [SerializeField] private float m_wanderInterval = 3f;

    private NavMeshAgent m_navMeshAgent;
    private FieldOfViewDetection2D m_fieldOfViewDetection;
    private EnemyState m_currentState = EnemyState.Idle;
    private float m_nextWanderTime;

    private void Start()
    {
        // get nav mesh component
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_fieldOfViewDetection = GetComponent<FieldOfViewDetection2D>();

        // editor checks
#if UNITY_EDITOR
        if (m_navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
        if (m_fieldOfViewDetection == null)
        {
            Debug.LogError("FieldOfViewDetection2D component not found on " + gameObject.name);
        }
#endif

        m_navMeshAgent.updateRotation = false;
        m_navMeshAgent.updateUpAxis = false;

        m_nextWanderTime = Time.time;
    }

    private void FixedUpdate()
    {
        // State checks
        if (m_fieldOfViewDetection.m_detected)
        {
            m_currentState = EnemyState.Attacking;
        }
        else
        {
            m_currentState = EnemyState.Idle;
        }

        // States
        switch (m_currentState) 
        {
            case EnemyState.Idle:
                // Wander around randomly
                if (Time.time >= m_nextWanderTime || !m_navMeshAgent.hasPath || m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
                {
                    SetWanderPos();
                    m_nextWanderTime = Time.time + m_wanderInterval;
                }
                break;

            case EnemyState.Attacking:
                // Chase the player
                m_navMeshAgent.SetDestination(m_fieldOfViewDetection.detectionRef.transform.position);
                break;

            default:
                break;
        }

        // update enemy rotation to face the movement direction
        if (m_navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            float angle = Mathf.Atan2(m_navMeshAgent.velocity.y, m_navMeshAgent.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }

    private void SetWanderPos()
    {
        // pick a random point within the wander radius
        Vector2 randomDirection = Random.insideUnitCircle * m_wanderRadius;
        Vector3 randomPosition = new Vector3(randomDirection.x, randomDirection.y, 0f) + transform.position;

        // check if the random position is on the nav mesh, set the destination if so
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, m_wanderRadius, NavMesh.AllAreas))
        {
            m_navMeshAgent.SetDestination(hit.position);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the wander radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_wanderRadius);
    }
}
