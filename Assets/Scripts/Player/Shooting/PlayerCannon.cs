using UnityEngine;

// The player cannon class is the main class for shooting
// This handles player input, aiming, shooting and cooldown management
// It refrances from the cannon type and cannon ability scriptable objects to determine how the cannon shoots and what abilities it has


public class PlayerCannon : MonoBehaviour
{
    [SerializeField] private CannonType m_cannonType;
}
