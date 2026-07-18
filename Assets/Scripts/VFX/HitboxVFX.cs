using System;
using UnityEngine;

public class HitboxVFX : MonoBehaviour
{
    [SerializeField] private float lifetime;
    
    private float _damageAmount;
    private Action<HitboxVFX> _releaseCallback;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= lifetime)
            _releaseCallback?.Invoke(this);
    }
    
    public void Init(float damage, Action<HitboxVFX> releaseCallback)
    {
        _damageAmount = damage;
        _releaseCallback = releaseCallback;
        _timer = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Hit {other.gameObject.name} for {_damageAmount}");
            
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_damageAmount);
            Debug.Log($"Hit {other.gameObject.name} for {_damageAmount}");
        }
    }
}
