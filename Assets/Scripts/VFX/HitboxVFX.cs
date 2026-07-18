using System;
using UnityEngine;

public class HitboxVFX : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private float travelSpeed;
    
    private float _damageAmount;
    private Action<HitboxVFX> _releaseCallback;
    private float _timer;
    
    private CharacterType _characterType;
    private Vector3 _targetPosition;
    private bool _isProjectile;

    private void OnEnable()
    {
        _timer = 0f;
    }
    
    private void Update()
    {
        if (_isProjectile)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, travelSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPosition) <= 0.05f)
                _releaseCallback?.Invoke(this);

            return;
        }

        _timer += Time.deltaTime;
        if (_timer >= lifetime)
            _releaseCallback?.Invoke(this);
    }
    
    // Melee / instant-hit version (player -> enemy)
    public void Init(float damage, Action<HitboxVFX> releaseCallback)
    {
        _damageAmount = damage;
        _releaseCallback = releaseCallback;
        _isProjectile = false;
        _timer = 0f;
    }

    // Projectile version (enemy -> player)
    public void InitProjectile(CharacterType characterType, float damage, Vector3 targetPosition, Action<HitboxVFX> releaseCallback)
    {
        _damageAmount = damage;
        _targetPosition = targetPosition;
        _releaseCallback = releaseCallback;
        _isProjectile = true;
        _timer = 0f;
        _characterType = characterType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Hit {other.gameObject.name} for {_damageAmount}");
        
        var character = other.GetComponent<Character>();
        
        if (character == null)
            return;
        
        if (character.CharacterType == _characterType)
        {
            return;
        }
        
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_damageAmount);
            Debug.Log($"Hit {other.gameObject.name} for {_damageAmount}");
        }
    }
}
