using UnityEngine;
using UnityEngine.InputSystem;

// class for basic player movement logic
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_moveDampening = 0.05f;
    [SerializeField] private InputActionReference m_movementActionReference;

    private Rigidbody2D m_rb;
    private Vector2 m_movement;
    private Vector2 m_targetVelocity;
    private Vector2 m_refVelocity = Vector2.zero;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_movement = m_movementActionReference.action.ReadValue<Vector2>();

        // Define the target velocity based on input
        m_targetVelocity = m_movement.normalized * m_moveSpeed;
    }

    private void FixedUpdate()
    {
        // Calculate the desired velocity after damping
        Vector2 desiredVelocity = Vector2.SmoothDamp(m_rb.linearVelocity, m_targetVelocity, ref m_refVelocity, m_moveDampening);

        // Calculate the change in velocity required to reach the desired velocity
        Vector2 velocityChange = desiredVelocity - m_rb.linearVelocity;

        // Convert the velocity change to a force. Force = mass * acceleration = mass * (delta_velocity / delta_time)
        Vector2 force = m_rb.mass * (velocityChange / Time.fixedDeltaTime);

        // Add the force to the Rigidbody
        m_rb.AddForce(force);
    }

    public Vector2 GetPlayerMovement()
    {
        return m_movement;
    }
}
