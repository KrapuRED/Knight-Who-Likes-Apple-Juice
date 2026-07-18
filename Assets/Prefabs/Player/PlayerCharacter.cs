using System;
using UnityEngine;

public class PlayerCharacter : Character, IDamageable
{
    [SerializeField] private float dodgeCost;
    [SerializeField] private float durationImmune;
    [SerializeField] private bool isImmune = false;
    private float _remainingImmune;

    public float DodgeCost => dodgeCost;
    
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

        CharacterHealth.UpdateHealthTakeDamage(damageValue);
    }
}
