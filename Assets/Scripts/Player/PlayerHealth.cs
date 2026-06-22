using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private TextMeshProUGUI m_healthText;
    public int CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    private void Awake()
    {
        CurrentHealth = maxHealth;

        // Set UI text
        if (m_healthText != null)
        {
            m_healthText.text = $"Health: {CurrentHealth}/{maxHealth}";
        }
    }

    public void TakeDamage(int damage)
    {
        if (IsDead || damage <= 0) return;
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        // Update UI text
        if (m_healthText != null)
        {
            m_healthText.text = $"Health: {CurrentHealth}/{maxHealth}";
        }

        if (IsDead) Die();
    }

    private void Die()
    {
        Debug.Log("Player died");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 2);
    }
}
