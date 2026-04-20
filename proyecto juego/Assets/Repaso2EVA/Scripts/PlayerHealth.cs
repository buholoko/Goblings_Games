using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    // Evento para avisar a otros scripts que morimos
    public event Action OnPlayerDeath;

    void Start()
    {
        ResetHealth();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
        ResetHealth(); // Opcional: curar al morir
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}