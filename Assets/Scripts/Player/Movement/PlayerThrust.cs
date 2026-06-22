using TMPro;
using UnityEngine;

// A player ability allowing the player to thrust in their moving direction providing a speed boost
// This will be attached to the player game object and will be used with the player movement script
// The ability will be activated by holding the space bar and will consume fuel. Fuel regenerates when not in use.

public class PlayerThrust : MonoBehaviour
{
    [Header("Thrust Settings")]
    [SerializeField][Range(0.0f, 100.0f)] private float m_thrustForce = 30f; // The continuous force applied when thrusting

    [Header("Fuel Tank")]
    [SerializeField] private float m_maxFuel = 100f; // The maximum amount of fuel
    [SerializeField] private float m_fuelConsumptionRate = 35f; // Fuel consumed per second while thrusting
    [SerializeField] private float m_fuelRegenerationRate = 20f; // Fuel regenerated per second when not thrusting

    [SerializeField] private TextMeshProUGUI m_thrustText;

    private float m_currentFuel;
    private bool m_isThrusting;

    private Rigidbody2D m_rb;
    private PlayerMovement m_playerMovement;

    // public properties to allow other scripts to access fuel status
    public float CurrentFuel => m_currentFuel;
    public float MaxFuel => m_maxFuel;

    private void Awake()
    {
        // get player rigidbody and movement script
        m_rb = GetComponent<Rigidbody2D>();
        m_playerMovement = GetComponent<PlayerMovement>();
        m_currentFuel = m_maxFuel; // Start with a full tank

        // ensure validity
        if (m_rb == null)
            Debug.LogError("PlayerThrust: Rigidbody2D component not found on player.");
        if (m_playerMovement == null)
            Debug.LogError("PlayerThrust: PlayerMovement component not found on player.");

        // UI
        if (m_thrustText != null)
        {
            m_thrustText.text = $"Thrust Fuel: {m_currentFuel:F1}/{m_maxFuel}";
        }
    }

    private void Update()
    {
        // check for thrust input
        m_isThrusting = Input.GetKey(KeyCode.Space);

        // regenerate fuel if not thrusting and fuel is not yet full
        if (!m_isThrusting && m_currentFuel < m_maxFuel)
        {
            m_currentFuel += m_fuelRegenerationRate * Time.deltaTime;
            m_currentFuel = Mathf.Min(m_currentFuel, m_maxFuel); // dont exceed max fuel
        }
    }

    private void FixedUpdate()
    {
        // apply thrust in FixedUpdate for consistent physics behavior
        if (m_isThrusting && m_currentFuel > 0)
        {
            ApplyThrust();
        }

        // Update UI
        if (m_thrustText != null)
        {
            m_thrustText.text = $"Thrust Fuel: {m_currentFuel:F1}/{m_maxFuel}";
        }
    }

    private void ApplyThrust()
    {
        if (m_playerMovement == null || m_rb == null)
        {
            Debug.LogError("PlayerThrust: Missing component references.");
            return;
        }

        Vector2 movingDirection = m_playerMovement.GetPlayerMovement();

        // only apply thrust if the player has input direction
        if (movingDirection.sqrMagnitude > 0.1f)
        {
            // Apply a continuous force in the direction of movement
            m_rb.AddForce(movingDirection.normalized * m_thrustForce, ForceMode2D.Force);

            // consume fuel
            m_currentFuel -= m_fuelConsumptionRate * Time.fixedDeltaTime;
            m_currentFuel = Mathf.Max(m_currentFuel, 0); // dont go below zero

            // Update UI
        }
    }
}
