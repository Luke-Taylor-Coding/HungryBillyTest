using UnityEngine;
using UnityEngine.Animations;

// The player cannon class is the main class for shooting
// This handles player input, aiming, shooting and cooldown management
// It refrances from the cannon type and cannon ability scriptable objects to determine how the cannon shoots and what abilities it has

public class PlayerCannon : MonoBehaviour
{
    [SerializeField] private CannonType m_cannonType;
    [SerializeField] private Transform m_firePoint;

    private float m_cooldownTimer;
    public Transform firePoint => m_firePoint;

    private void Update()
    {
        // Handle aiming the cannon based on mouse position
        Aim();

        // Cooldown and shooting logic
        m_cooldownTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && m_cooldownTimer <= 0)
        {
            Shoot();
        }    
    }

    /// <summary>
    /// Handles aiming logic for the cannon
    /// Uses the mouse position to determine the direction to aim, and rotates the cannon accordingly
    /// </summary>
    private void Aim()
    {
        // convert mouse screen pos to world at the cannon's Z plane
        Vector3 mouseScreen = Input.mousePosition;
        float camToObject = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        mouseScreen.z = camToObject;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreen);

        // direction and angle
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// Handles shooting logic for the cannon, uses the cannon type and abilities to determine how to shoot, and resets the cooldown timer
    /// </summary>
    private void Shoot()
    {
        // ensure cannon type
        if (m_cannonType == null)
        {
            Debug.LogError("Cannon type is not assigned!");
            return;
        }

        // call to cannon ability to activate the ability (fire the projectiles)
        m_cannonType.Ability.Activate(m_cannonType, m_firePoint);

        // reset cooldown timer based on cannon types fire rate
        m_cooldownTimer = m_cannonType.Cooldown;
    }

    /// <summary>
    /// An exposed function for setting the cannon type 
    /// </summary>
    public void SetCannonType(CannonType newType)
    {
        m_cannonType = newType;
    }
}
