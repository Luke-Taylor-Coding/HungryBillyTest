using UnityEngine;

// A universal object pool class that can be used to manage and reuse game objects
// An initial prefab game object and pool size are set, instantiating a pool of objects at the start of the game
// All game objects in the pool are deactivated on start
// Functions are provided to manage the pool and its objects

// HOW TO USE:
// 1. Create a new empty game object in the scene and attach this ObjectPool script
// 2. In the inspector, set the prefab you want to pool and the initial pool size
// 3. Call the SpawnFromPool() function to spawn the first object availbile at a set position and rotation
// 4. When calling this a refrance to the game object is returned to manipulate the objec
// 5. Either use the object refrance or have the prefab self manage for deactivation


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField][Range(1, 1000)] private int _poolSize = 1;

    private void Awake()
    {
        // Ensure prefab is set
        if (_gameObjectPrefab == null)
        {
            Debug.LogError("ObjectPool: No prefab set for pooling. Please assign a prefab in the inspector.");
            return;
        }

        // instantiate pool of objects and deactivate them
        for (int i = 0; i < _poolSize; i++)
        {
            Instantiate(_gameObjectPrefab, transform).SetActive(false);
        }
    }

    ///<summary>
    /// Function to spawn an object from the pool, activating it and returning a reference to it
    ///</summary>
    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        // find the first inactive object in the pool
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy)
            {
                // activate and position the object before returning it
                child.position = position;
                child.rotation = rotation;
                child.gameObject.SetActive(true);
                return child.gameObject;
            }
        }
        // if no inactive objects are available, log a warning and return null
        Debug.LogWarning("ObjectPool: No inactive objects available in the pool. Consider increasing the pool size.");
        return null;
    }
}
