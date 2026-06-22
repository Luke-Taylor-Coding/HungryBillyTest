using UnityEngine;

// This scriptable object represents the type of cannon the player can use 
// It contains all information needed about the cannon , including its name, description, projectile prefab, cooldown and ability
// Used by the PlayerCannon class to determine how the cannon shoots and what abilities it has

[CreateAssetMenu(fileName = "CannonType", menuName = "CannonType/CannonType")]
public class CannonType : ScriptableObject
{
    [Header("Cannon Properties")]
    [SerializeField] private string m_cannonName;
    [SerializeField][TextArea] private string m_description;

    [Header("Shooting Properties")]
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_cooldown = 0.5f;

    [Header("Abilities")]
    [SerializeField] private CannonAbility m_ability;


    // Public accessors
    public string CannonName => m_cannonName;
    public string Description => m_description;
    public GameObject ProjectilePrefab => m_projectilePrefab;
    public float Cooldown => m_cooldown;
    public CannonAbility Ability => m_ability;
}
