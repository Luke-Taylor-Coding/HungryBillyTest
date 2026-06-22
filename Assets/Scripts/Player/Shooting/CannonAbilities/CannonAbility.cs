using UnityEngine;

// This scriptable object represents the ability of the cannon
// It contains all information needed about the ability, including its name, description and effect
// Called by the PlayerCannon class to use the ability functionality

public abstract class CannonAbility : ScriptableObject
{
    [SerializeField] private string m_abilityName;
    [SerializeField][TextArea] private string m_description;

    public string AbilityName => m_abilityName;
    public string Description => m_description;

    public abstract void Activate(CannonType cannonType, Transform firePoint);
}
