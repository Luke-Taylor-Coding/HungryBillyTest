using UnityEngine;

// This script manages the collection items in the game
// These items are use to win the game and can be collected by the player
// This script uses an array of item positions to spawn collection items in the game
// Once all these are collected the player has won the game and the game will end

public class CollectionItemManager : MonoBehaviour
{
    [SerializeField] private GameObject[] m_spawnPositions;

    public static CollectionItemManager s_instance { get; private set; } // Singleton instance

    private int m_totalItemsToCollect = 0;
    private int m_collectedCount = 0;

    void Start()
    {
        // Singleton logic
        if (s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        s_instance = this;

        if (m_spawnPositions == null || m_spawnPositions.Length == 0)
        {
            Debug.LogError("CollectionItemManager: No spawn positions assigned.");
            return;
        }

        m_totalItemsToCollect = m_spawnPositions.Length;
    }

    /// <summary>
    /// Updates the count of collected items and checks if the player has collected all items to win the game
    /// </summary>
    public void CollectItem()
    {
        m_collectedCount++;
        Debug.Log($"Collected {m_collectedCount}/{m_totalItemsToCollect} items.");
        if (m_collectedCount >= m_totalItemsToCollect)
        {
            Debug.Log("All items collected! You win!");
        }
    }

}
