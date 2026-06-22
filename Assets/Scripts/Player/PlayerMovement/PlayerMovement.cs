using UnityEngine;
// class for baisc player movement logic

public class Player : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5f;
    private Rigidbody2D m_rb;
    private Vector2 m_movement;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // get input using the new input system (WASD or Arrow Keys)
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        m_rb.MovePosition(m_rb.position + m_movement.normalized * m_moveSpeed * Time.fixedDeltaTime);
    }
}
