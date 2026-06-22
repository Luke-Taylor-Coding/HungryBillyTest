using UnityEngine;

// This class is used by the planets in the scene to create a gravity field around them
// When a player enters the gravity field (a trigger collider) the player's RB will be pulled towards the planet's center of mass, simulating gravity

public class PlanetGravityField : MonoBehaviour
{
    [SerializeField][Range(0.0f, 500.0f)] private float m_GravityEffect = 10.0f;
    [SerializeField][Range(0.0f, 100.0f)] private float m_GravityDistanceMultiplier = 1.0f;

    private Collider2D m_triggerCollider;

    private void Awake()
    {
        // Grab the trigger collider component
        m_triggerCollider = GetComponent<Collider2D>();

        if (m_triggerCollider == null)
        {
            Debug.LogError("PlanetGravityField requires a Collider2D component to function properly.");
            return;
        }
    }

    ///<summary>
    /// Called when another collider enters and stays in the gravity area, filters for players, affecting their Rigidbody2D to pull them towards the planet's center
    /// Note: This method assumes the player has a Rigidbody2D component and is tagged with "Player"
    ///</summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the colliding object is tagged as "Player"
        if (collision.CompareTag("Player"))
        {
            // Get player RB, ensure it is valid
            Rigidbody2D playerRB = collision.GetComponent<Rigidbody2D>();
            if (playerRB == null)
            {
                Debug.LogError("Player object does not have a Rigidbody2D component.");
                return;
            }

            // calculate gravity direction
            Vector2 gravityDirection = (transform.position - collision.transform.position).normalized;

            // calculate distance between player and planet center
            float distance = Vector2.Distance(transform.position, collision.transform.position);

            // increase gravity effect based on distance
            float adjustedGravityEffect = m_GravityEffect * m_GravityDistanceMultiplier / (distance * distance);

            // apply gravity force to player RB
            playerRB.AddForce(gravityDirection * adjustedGravityEffect, ForceMode2D.Force);
        }
    }
}
