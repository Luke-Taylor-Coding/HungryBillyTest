using UnityEngine;
using System.Collections.Generic;

// This is a central registry class for managing all object pools in the game
// It allows for easy access to different object pools and their respective objects
// NOTE: Object pools will automatically attempt to register themselves to this manager on creation


public class ObjectPoolsManager : MonoBehaviour
{
    public static ObjectPoolsManager s_instance { get; private set; } // Singleton instance

    private readonly Dictionary<GameObject, ObjectPool> m_pools = new Dictionary<GameObject, ObjectPool>(); // Dictionary to hold object pools


    private void Awake()
    {
        // Singleton logic
        if(s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        s_instance = this;
    }

    /// <summary>
    /// To be called by Object Pool instnces to register themselves
    /// </summary>
    public void RegisterPool(ObjectPool pool)
    {
        if (pool.GetPrefab != null && !m_pools.ContainsKey(pool.GetPrefab))
        {
            m_pools.Add(pool.GetPrefab, pool);
        }
        else
        {
            Debug.LogWarning($"Attempted to register a pool with a null prefab or a prefab that is already registered: {pool.GetPrefab}");
        }

    }

    /// <summary>
    /// Retruns a referance to the object pool associated with the given GameObject prefab
    /// </summary>
    public ObjectPool GetPool(GameObject pool)
    {
        m_pools.TryGetValue(pool, out ObjectPool objectPool);
        return objectPool;
    }
}
