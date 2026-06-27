using UnityEngine;
using UnityEngine.InputSystem;

// The player cannon class is the main class for shooting
// This handles player input, aiming, shooting and cooldown management
// It refrances from the cannon type and cannon ability scriptable objects to determine how the cannon shoots and what abilities it has

public class PlayerCannon : MonoBehaviour
{
    [SerializeField] private CannonType m_cannonType;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private InputActionReference m_attackActionReference;
    [SerializeField] private InputActionReference m_lookActionReference;

    private float m_cooldownTimer;
    private bool m_isFiring;
    public Transform firePoint => m_firePoint;

    private void OnEnable()
    {
        m_attackActionReference.action.started += OnFireStarted;
        m_attackActionReference.action.canceled += OnFireStopped;
    }

    private void OnDisable()
    {
        m_attackActionReference.action.started -= OnFireStarted;
        m_attackActionReference.action.canceled -= OnFireStopped;
    }

    private void FixedUpdate()
    {
        Aim();

        if (m_cooldownTimer > 0)
        {
            m_cooldownTimer -= Time.fixedDeltaTime;
        }

        if (m_isFiring && m_cooldownTimer <= 0)
        {
            Shoot();
        }
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        m_isFiring = true;
    }

    private void OnFireStopped(InputAction.CallbackContext context)
    {
        m_isFiring = false;
    }

    /// <summary>
    /// Handles aiming logic for the cannon
    /// Uses the mouse position to determine the direction to aim, and rotates the cannon accordingly
    /// </summary>
    private void Aim()
    {
        // convert mouse screen pos to world at the cannon's Z plane
        Vector3 mouseScreen = m_lookActionReference.action.ReadValue<Vector2>();
        float camToObject = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        mouseScreen.z = camToObject;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouseScreen);

        // direction and angle
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        transform.rotation = rotation;
        firePoint.localRotation = Quaternion.Euler(0, 0, 90);
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
