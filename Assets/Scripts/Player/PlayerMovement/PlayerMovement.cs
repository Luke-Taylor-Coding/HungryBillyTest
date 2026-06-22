using UnityEngine;

// class for basic player movement logic
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_moveDampening = 0.05f;

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
        // get input using the new input system (WASD or Arrow Keys)
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        // Define the target velocity based on input
        m_targetVelocity = m_movement.normalized * m_moveSpeed;
    }

    private void FixedUpdate()
    {
        // Smoothly change the current velocity towards the target velocity.
        m_rb.linearVelocity = Vector2.SmoothDamp(m_rb.linearVelocity, m_targetVelocity, ref m_refVelocity, m_moveDampening);
    }
}
