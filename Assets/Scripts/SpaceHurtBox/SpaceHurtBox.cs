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
    [SerializeField] private Color safeColor = Color.green;
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color dangerColor = Color.red;

    [SerializeField] private List<ActiveAttack> _activeAttacks = new();
    
    private float _currentTime;
    private bool _hasWarned;
    private bool _isSpent = true;
    
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
                attack.owner?.CharacterAnimation.AnticipationAnimation();
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
            indicatorSr.color = Color.white;
        else if (anyDangerous)
            indicatorSr.color = dangerColor;
        else if (anyWarning)
            indicatorSr.color = warningColor;
        else
            indicatorSr.color = safeColor;
    }
    
    public void ActivateHitBox(Character owner, float damage)
    {
        _activeAttacks.Add(new ActiveAttack
        {
            owner = owner,
            damageAmount = damage,
            currentTime = 0f,
            hasWarned = false
        });
    }

    private void OnExpired()
    {
        _isSpent = true;
        indicatorSr.color = Color.white;
    }
    
    private void CheckOverlapAndDamage(ActiveAttack attack)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPoint.position, sizeRadius, targetLayer);

        if (colliders.Length <= 0)
            return;

        var damageable = colliders[0].GetComponent<IDamageable>();
        if (damageable == null)
            return;

        attack.owner?.CharacterAnimation.AttackAnimation();
        damageable.TakeDamage(attack.damageAmount);
    }

    private void ExecuteDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damageAmount);
        _isSpent = true;
        indicatorSr.color = safeColor;
    }

    private void ResetSpaceHurtBox()
    {
        indicatorSr.color = safeColor;
        _currentTime = 0f;
        _hasWarned = false;
        _isSpent = false;
    }
}
