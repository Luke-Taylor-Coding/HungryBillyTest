using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfViewDetection2D))]
public class FieldOfViewDetection2DEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfViewDetection2D fovDetection = (FieldOfViewDetection2D)target;

        Vector3 origin = fovDetection.transform.position;
        Vector3 forward = fovDetection.transform.up;

        Handles.color = Color.red;
        Handles.DrawWireArc(origin, Vector3.forward, forward, 360, fovDetection.radius);

        Vector3 leftBoundary = origin + Quaternion.Euler(0, 0, -fovDetection.fovAngle / 2f) * forward * fovDetection.radius;
        Vector3 rightBoundary = origin + Quaternion.Euler(0, 0, fovDetection.fovAngle / 2f) * forward * fovDetection.radius;

        Handles.color = Color.yellow;
        Handles.DrawLine(origin, leftBoundary);
        Handles.DrawLine(origin, rightBoundary);

        if (fovDetection.m_detected)
        {
            Handles.color = Color.green;
            Handles.DrawLine(origin, fovDetection.detectionRef.transform.position);
        }
    }
}
