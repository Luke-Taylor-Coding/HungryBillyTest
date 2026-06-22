using UnityEngine;

// When ESC is pressed the game UI will appear

public class UIControls : MonoBehaviour
{
    [SerializeField] private GameObject m_gameUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the game UI
            m_gameUI.SetActive(!m_gameUI.activeSelf);
        }
    }
}
