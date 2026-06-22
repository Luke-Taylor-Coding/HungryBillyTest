using UnityEngine;

// This script represents a multi-shot item in the game
// On collision with the player, it will be collected and removed from the game
// giving the player the multi-shot ability

public class MultiShotPickup : MonoBehaviour
{
    [SerializeField] private ScriptableObject m_multiShotCannonType; // Reference to the multi-shot cannon type

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // get player cannon and set the ability to multi-shot
            collision.GetComponent<PlayerCannon>().SetCannonType((CannonType)m_multiShotCannonType);
            Destroy(this.gameObject);
        }
    }
}
