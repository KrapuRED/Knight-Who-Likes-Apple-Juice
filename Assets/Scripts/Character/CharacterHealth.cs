using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public Character owenrCharacter;
    public float maxHealth;
    public float currentHealth;

    [SerializeField] private StatusBarUI healthBarUI;
    
    private void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.UpdateStatusBar(currentHealth, maxHealth);
    }

    public void UpdateHealthTakeDamage(float amount)
    {
        currentHealth = Mathf.Min(currentHealth - amount, maxHealth);
    
        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} is dead");
            owenrCharacter.OnDeadCharacter();
        }
        
        healthBarUI.UpdateStatusBar(currentHealth, maxHealth);
        
    }

    public void UpdateHealthRestore(float amount)
    {
        Debug.Log($"{gameObject.name} is restored {amount}");
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        healthBarUI.UpdateStatusBar(currentHealth, maxHealth);
        
    }
}
