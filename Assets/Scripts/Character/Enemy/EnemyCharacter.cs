using System;
using UnityEngine;

public class EnemyCharacter : Character, IDamageable
{
    [Header("Attack bar Config")]
    [SerializeField] protected float maxAttackBar;
    [SerializeField] protected float currentAttackBar;
    [SerializeField] protected float rateAttackBar;
    [SerializeField] protected float damageValue;

    public float DamageValue => damageValue;
    
    private void Update()
    {
        if (currentAttackBar >= maxAttackBar)
            return;
        
        currentAttackBar += Time.deltaTime * rateAttackBar;
    }

    public void TakeDamage(float amountDamage)
    {
        CharacterHealth.UpdateHealthTakeDamage(amountDamage);
    }

    public void ResetCondition() => currentAttackBar = 0;
    
    public bool IsAttackBarFull() => currentAttackBar >= maxAttackBar;
}
