using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public Character owenrCharacter;
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private string soundEffect;

    [SerializeField] private HeallthStatusBarUI healthBarUI;
    
    private void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.UpdateStatusBar(currentHealth, maxHealth);
    }

    public void UpdateHealthTakeDamage(float amount)
    {
        currentHealth = Mathf.Min(currentHealth - amount, maxHealth);
    
        if (!string.IsNullOrEmpty(soundEffect))
            SoundEffectManager.Instance.PlaySound2D(soundEffect);
        
        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} is dead");
            owenrCharacter.OnDeadCharacter();
        }
        
        healthBarUI.UpdateStatusBar(currentHealth, maxHealth);
        
    }

    public void UpdateHealthRestore(float amount)
    {
        owenrCharacter.CharacterAnimation.PlayHealAnimation();
        SoundEffectManager.Instance.PlaySound2D("player_heal");
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        healthBarUI.UpdateStatusBar(currentHealth, maxHealth);
        
    }
}
