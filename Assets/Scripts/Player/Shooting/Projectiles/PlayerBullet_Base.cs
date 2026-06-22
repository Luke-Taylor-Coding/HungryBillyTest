using UnityEngine;

// Logic for the base player bullet
// Bullet will move at a constant speed in the direction it was fired (Handled by the cannon)
// Bullet will have a lifetime and will be destroyed after that time (Handled by the bullet prefab itself)
// Bullet will have a damage value and will deal damage to enemies on collision
// NOTE: When a bullet is 'fired' it is enabled from a pool, therefore most logic is based on onEnable events

public class PlayerBullet_Base : MonoBehaviour
{
    [SerializeField] private float m_bulletSpeed = 10.0f;
    [SerializeField] private float m_lifetime = 5.0f; // Lifetime in seconds before the bullet is disabled (returned to the pool)

    private Rigidbody2D m_rb;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        if (m_rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the bullet prefab!");
        }
    }

    /// <summary>
    /// This method is called when the bullet is enabled (fired) from the pool
    /// It handles the bullets movement and lifetime
    /// NOTE: By this point the bullet has already been positioned and rotated by the cannon
    /// </summary>
    private void OnEnable()
    {
        // apply force to the rigidbody in the direction the bullet is facing (force only needs to apply once as bullet RB has no drag or gravity)
        if (m_rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the bullet prefab!");
            return;
        }

        m_rb.AddForce(transform.right * m_bulletSpeed, ForceMode2D.Impulse);

        // disable bullet after lifetime
        Invoke(nameof(DisableBullet), m_lifetime);
    }

    private void DisableBullet()
    {
        // disable the bullet (return to pool)
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when the bullet collides with another object, only filters for enemies and applies damage to them
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO
    }
}
