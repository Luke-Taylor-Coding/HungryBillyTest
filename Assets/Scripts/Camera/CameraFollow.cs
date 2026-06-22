using UnityEngine;

// This class handles camera follow logic for the player
// The camera will smoothly follow the player's position with a slight delay
// script to be attached to the main camera

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private float m_followSpeed = 5f;

    void Start()
    {
        // check if playerTransform is assigned
        if (m_playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned in CameraFollow script.");
            return;
        }

        // ensure the camera starts at the player's position 
        Vector3 startPosition = m_playerTransform.position;
        startPosition.z = transform.position.z; // maintain camera's z position
        transform.position = startPosition;

    }

    void Update()
    {
        // update camera position to follow player with a slight delay
        Vector3 targetPosition = m_playerTransform.position;
        targetPosition.z = transform.position.z; // maintain camera's z position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * m_followSpeed);
    }
}
