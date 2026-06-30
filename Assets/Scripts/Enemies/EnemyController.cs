using UnityEngine;
using UnityEngine.AI;

// A controller script for the basic enemy behavior
// Current basic behavior involves wandering and attacking / chasing the player when in rage
// Uses unity NavMeshAgent for pathfinding and movement

// Player detection logic
// Enemy has a FOV range, this is a cone detection that checks for the player tag
// On finidng a player tag it will raycast to check if the player is in line of sight
// If so it will set the player as the target and start chasing / attacking

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent m_navMeshAgent;

    private void Start()
    {
        // get nav mesh component
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        // editor checks
#if UNITY_EDITOR
        if (m_navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
#endif

        m_navMeshAgent.updateRotation = false;
        m_navMeshAgent.updateUpAxis = false;
    }

   
}
