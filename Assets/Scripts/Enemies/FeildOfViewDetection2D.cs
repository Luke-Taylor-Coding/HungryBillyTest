using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;

// A component that detects objects within a field of view (FOV) in 2D space
// Tags can be set to filter which objects are detected 
// Events can be assigned to trigger on detection of objects within the FOV
// Events pass back the detected object
// Handles are drawn to show the radius, FOV angle and when an object is detected
// NOTE: colision checks are staggered using a corutine every set interval to reduce performance impact

public class FieldOfViewDetection2D : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float m_radius;
    [SerializeField][Range(0, 360)] private int m_fovAngle;
    [SerializeField] private float m_detectionInterval = 0.2f;

    [Header("Detection Layers")]
    [SerializeField] private LayerMask m_detectionLayer;
    [SerializeField] private LayerMask m_obstructionLayer;

    [Header("Events")]
    [SerializeField] private UnityEvent<GameObject> m_onDetectObject;

    private GameObject m_detectionRef;

    public bool m_detected;
    public float radius => m_radius;
    public int fovAngle => m_fovAngle;
    public GameObject detectionRef => m_detectionRef;

    private void Start()
    {
        StartCoroutine(DetectObjects());
    }

    private IEnumerator DetectObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_detectionInterval);
            DetectionCheck();
        }
    }

    private void DetectionCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_radius, m_detectionLayer);

        if (colliders.Length != 0)
        {
            Transform target = colliders[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < m_fovAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, m_obstructionLayer))
                {
                    m_detected = true;
                    m_detectionRef = target.gameObject;
                    m_onDetectObject?.Invoke(m_detectionRef);
                }
                else
                {
                    m_detected = false;
                    m_detectionRef = null;
                }
            }
            else
            {
                m_detected = false;
                m_detectionRef = null;
            }
        }
        else if(m_detected)
        {
            m_detected = false;
            m_detectionRef = null;
        }
    }
}
