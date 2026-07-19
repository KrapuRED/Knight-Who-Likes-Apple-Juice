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

    private void Start()
    {
        GameManager.Instance.AddCharacter(this);
    }

    private void Update()
    {
        currentAttackBar += Time.deltaTime * rateAttackBar;
    
        // Kunci nilainya agar wajib berada di antara 0 dan maxAttackBar
        currentAttackBar = Mathf.Clamp(currentAttackBar, 0f, maxAttackBar);

        // Tentukan status full atau tidak
        isAttackBarFull = (currentAttackBar >= maxAttackBar);
    }

    public void TakeDamage(float amountDamage)
    {
        CharacterHealth.UpdateHealthTakeDamage(amountDamage);
    }

    public void ResetCondition() => currentAttackBar = 0;
}
