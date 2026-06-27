using UnityEngine;
using UnityEngine.InputSystem;

// When ESC is pressed the game UI will appear

public class UIControls : MonoBehaviour
{
    [SerializeField] private GameObject m_gameUI;
    [SerializeField] private InputActionAsset m_uiActionAsset;

    void Update()
    {
        if (m_uiActionAsset.FindAction("ToggleUI").triggered)
        {
            // Toggle the game UI
            m_gameUI.SetActive(!m_gameUI.activeSelf);
        }
    }
}
