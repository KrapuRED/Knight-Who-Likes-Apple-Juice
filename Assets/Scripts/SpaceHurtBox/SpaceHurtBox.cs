using System;
using UnityEngine;
using System.Collections.Generic;

public class SpaceHurtBox : MonoBehaviour
{
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

    private float _currentTime;
    private bool _hasWarned;
    private bool _isSpent;
    
    private void Update()
    {

        if (_isSpent)
            return; 

        _currentTime += Time.deltaTime;

        float timeRemaining = timerActive - _currentTime;

        if (!_hasWarned && timeRemaining <= warningThreshold)
        {
            _hasWarned = true;
            indicatorSr.color = warningColor;
        }

        if (_currentTime >= timerActive)
        {
            indicatorSr.color = dangerColor;
            CheckOverlapAndDamage();
        }
        
        if (_currentTime >= expriedTime)
            OnExpired();
    }

    private void OnExpired()
    {
        _isSpent = true;
        indicatorSr.color = safeColor;
    }
    
    private void CheckOverlapAndDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPoint.position, sizeRadius, targetLayer);
        
        if (colliders.Length <= 0)
            return;
        
        var damageable = colliders[0].GetComponent<IDamageable>();
        if (damageable == null)
            return;
        
        ExecuteDamage(damageable);
    }

    private void ExecuteDamage(IDamageable damageable)
    {
        damageable.TakeDamage(damageAmount);
        _isSpent = true;
        indicatorSr.color = safeColor;
        Debug.Log($"Berhasil memberi damage sebesar {damageAmount} ke {((MonoBehaviour)damageable).gameObject.name}");
    }

    public void ResetSpaceHurtBox()
    {
        Debug.Log("ResetSpaceHurtBox: " + gameObject.transform.parent.name);
        indicatorSr.color = safeColor;
        _currentTime = 0f;
        _hasWarned = false;
        _isSpent = false;
    }

    private void OnDrawGizmos()
    {
        
    }
}
