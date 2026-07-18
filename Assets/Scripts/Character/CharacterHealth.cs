using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public Character owenrCharacter;
    public float maxHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealthTakeDamage(float amount)
    {
        currentHealth = Mathf.Min(currentHealth - amount, maxHealth);
    
        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} is dead");
            owenrCharacter.OnDeadCharacter();
        }
    }

    public void UpdateHealthRestore(float amount)
    {
        Debug.Log($"{gameObject.name} is restored {amount}");
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
