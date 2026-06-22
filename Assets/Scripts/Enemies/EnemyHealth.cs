using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    public int CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead || damage <= 0) return;
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        if (IsDead) Die();
    }

    private void Die()
    {
        Debug.Log($"{name} died");
        Destroy(gameObject);
    }
}
