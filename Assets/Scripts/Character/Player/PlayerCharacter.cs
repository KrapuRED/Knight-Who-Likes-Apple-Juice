using System;
using UnityEngine;

public class PlayerCharacter : Character, IDamageable
{
    [SerializeField] private float durationImmune;
    [SerializeField] private bool isImmune = false;
    private float _remainingImmune;

    private void Update()
    {
        if (_remainingImmune <= 0)
            isImmune = false;
        
        if (_remainingImmune >= 0)
        {
            _remainingImmune -= Time.deltaTime;
        }
    }

    public void OnDodge()
    {
        if (isImmune)
        {
            return;
        }
        
        isImmune = true;
        _remainingImmune = durationImmune;
    }
    
    public void TakeDamage(float damageValue)
    {
        if (isImmune)
            return;
        
        Debug.Log($"{gameObject.name} is taking damage {damageValue}");
        CharacterHealth.UpdateHealthTakeDamage(damageValue);
    }
}
