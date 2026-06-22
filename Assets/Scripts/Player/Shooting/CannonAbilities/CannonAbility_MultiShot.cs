using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonAbility_MultiShot", menuName = "CannonAbilities/MultiShot")]
public class MultiShotAbility : CannonAbility
{
    public int numberOfProjectiles = 3;
    public float spreadAngle = 15f;

    public override void Activate(CannonType cannonType, Transform firePoint)
    {
        // Fires multiple projectiles in a spread pattern. The middle projectile goes straight,
        // others are angled left/right based on the spread angle.
        // NOTE: the projectile behaviour and movement logic is all handled by the prefab itself

        // calculate middle offset so angles are symmetric around the forward direction
        float middleOffset = (numberOfProjectiles - 1) * 0.5f;

        var pool = ObjectPoolsManager.s_instance.GetPool(cannonType.ProjectilePrefab);

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // angle relative to the firePoint's forward rotation
            float angle = (i - middleOffset) * spreadAngle;
            Quaternion spawnRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, angle);

            // spawn each projectile with the calculated rotation
            pool.SpawnFromPool(firePoint.position, spawnRotation);
        }
    }
}
