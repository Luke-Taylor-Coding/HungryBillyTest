using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_maxHealth = 3;
    [SerializeField] private int m_currentHealth;

    [Header("Events")]
    [SerializeField] private UnityEvent<int, int> m_onHealthChanged = new UnityEvent<int, int>();
    [SerializeField] private UnityEvent m_onDied = new UnityEvent();

    public int CurrentHealth => m_currentHealth;
    public int MaxHealth => m_maxHealth;
    public bool IsDead => m_currentHealth <= 0;

    private void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || IsDead)
            return;

        m_currentHealth = Mathf.Max(m_currentHealth - amount, 0);
        m_onHealthChanged.Invoke(m_currentHealth, m_maxHealth);

        if (IsDead)
        {
            m_onDied.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || IsDead)
            return;

        m_currentHealth = Mathf.Min(m_currentHealth + amount, m_maxHealth);
        m_onHealthChanged.Invoke(m_currentHealth, m_maxHealth);
    }

    public void SetMaxHealth(int maxHealth)
    {
        m_maxHealth = Mathf.Max(maxHealth, 1);
    }

    public void SetCurrentHealth(int currentHealth, bool invokeEvents = false)
    {
        m_currentHealth = Mathf.Clamp(currentHealth, 0, m_maxHealth);
        if (invokeEvents)
        {
            m_onHealthChanged.Invoke(m_currentHealth, m_maxHealth);
            if (IsDead)
            {
                m_onDied.Invoke();
            }
        }
    }

    public void ResetHealth(bool invokeEvents = false)
    {
        m_currentHealth = m_maxHealth;
        if (invokeEvents)
        {
            m_onHealthChanged.Invoke(m_currentHealth, m_maxHealth);
        }
    }
}
