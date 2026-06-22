using UnityEngine;

[CreateAssetMenu(fileName = "CannonAbility_MultiShot", menuName = "CannonAbilities/MultiShot")]
public class MultiShotAbility : CannonAbility
{
    public int numberOfProjectiles = 3;
    public float spreadAngle = 15f;

    public override void Activate(PlayerCannon playerCannon)
    {
        // Logic for firing multiple projectiles
    }
}
