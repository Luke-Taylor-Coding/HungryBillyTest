using UnityEngine;

// This script represents a collection item in the game
// On collision with the player, it will be collected and removed from the game
// Calling back to the manager to update the collection count and check if the player has won the game

public class CollectionItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectionItemManager.s_instance.CollectItem();
            Destroy(this.gameObject);
        }
    }
}
