using UnityEngine;

[CreateAssetMenu(fileName = "CannonAbility_Base", menuName = "CannonAbilities/Base")]
public class BaseAbility : CannonAbility
{
    public override void Activate(CannonType cannonType, Transform firePoint)
    {
        // Single Shot fires a single projectile from the cannon's fire point
        // NOTE: the projectile behaviour and movement logic is all handled by prefab itself

        // grab the projectile prefab from the cannon type and find the relavant pool, spawn the projectile at the fire point's position and rotation
        var projectile = ObjectPoolsManager.s_instance.GetPool(cannonType.ProjectilePrefab).SpawnFromPool(firePoint.position, firePoint.rotation);
    }
}
