using System;
using UnityEngine;

public class EnemyCharacter : Character, IDamageable
{
    [Header("Attack bar Config")]
    [SerializeField] private float maxAttackBar;
    [SerializeField] private float currentAttackBar;
    [SerializeField] private float rateAttackBar;

    private void Update()
    {
        if (currentAttackBar >= maxAttackBar)
            return;
        
        currentAttackBar += Time.deltaTime * rateAttackBar;
    }

    public void TakeDamage(float damageValue)
    {
        Debug.Log($"{gameObject.name} is taking damage {damageValue}");
        CharacterHealth.UpdateHealthTakeDamage(damageValue);
    }

    public void ResetCondition() => currentAttackBar = 0;
    
    public bool IsAttackBarFull() => currentAttackBar >= maxAttackBar;
}
