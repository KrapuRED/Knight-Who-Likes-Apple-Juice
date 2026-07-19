using System;
using UnityEngine;
using System.Collections.Generic;

public class SpaceHurtBox : MonoBehaviour
{
    [System.Serializable]
    private class ActiveAttack
    {
        public Character owner;
        public float damageAmount;
        public float currentTime;
        public bool hasWarned;
        public PointDirection targetDirection;
        public int attackID;
    }
    
    private Character _spaceOwner;
    
    [SerializeField] private float expriedTime;
    [SerializeField] private float timerActive;       
    [SerializeField] private float warningThreshold = 0.5f; 
    [SerializeField] private float damageAmount;
    [SerializeField] private LayerMask targetLayer;

    [SerializeField] private Transform centerPoint;
    [SerializeField] private float sizeRadius;
    [SerializeField] private SpriteRenderer indicatorSr;
    [SerializeField] private Animator indicatorAnim;

    [SerializeField] private List<ActiveAttack> _activeAttacks = new();
    
    private void Update()
    {
        if (_activeAttacks.Count == 0)
            return;

        for (int i = _activeAttacks.Count - 1; i >= 0; i--)
        {
            ActiveAttack attack = _activeAttacks[i];
            attack.currentTime += Time.deltaTime;

            float timeRemaining = timerActive - attack.currentTime;

            if (!attack.hasWarned && timeRemaining <= warningThreshold)
            {
                attack.hasWarned = true;
                attack.owner?.CharacterAnimation.AnticipationAnimation(new[] { attack.targetDirection }, attack.attackID);
            }

            if (attack.currentTime >= timerActive)
            {
                CheckOverlapAndDamage(attack);
                _activeAttacks.RemoveAt(i);
                continue;
            }

            if (attack.currentTime >= expriedTime)
            {
                attack.owner?.CharacterAnimation.IdleAnimation();
                _activeAttacks.RemoveAt(i);
            }
        }

        UpdateIndicatorColor();
    }
    
    private void UpdateIndicatorColor()
    {
        bool anyDangerous = false;
        bool anyWarning = false;

        foreach (var attack in _activeAttacks)
        {
            if (attack.currentTime >= timerActive)
                anyDangerous = true;
            else if (attack.hasWarned)
                anyWarning = true;
        }

        if (_activeAttacks.Count == 0)
            indicatorSr.sprite = null;
        else if (anyWarning)
        {
            indicatorAnim.SetTrigger("Anticipation");
        }
    }
    
    public void ActivateHitBox(Character owner, float damage, PointDirection direction, int indexAttack)
    {
        Debug.Log($"Activating hitbox {owner.name} with attack index {indexAttack}");
        
        _activeAttacks.Add(new ActiveAttack
        {
            owner = owner,
            damageAmount = damage,
            currentTime = 0f,
            hasWarned = false,
            targetDirection = direction,
            attackID = indexAttack 
        });
    }
    
    private void CheckOverlapAndDamage(ActiveAttack attack)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPoint.position, sizeRadius, targetLayer);

        if (colliders.Length <= 0)
            return;

        var damageable = colliders[0].GetComponent<IDamageable>();
        if (damageable == null)
            return;

        attack.owner?.CharacterAnimation.AttackAnimation(new[] { attack.targetDirection }, attack.attackID);
        damageable.TakeDamage(attack.damageAmount);
    }
}
