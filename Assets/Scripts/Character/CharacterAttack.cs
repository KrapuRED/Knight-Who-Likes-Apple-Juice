using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Transform target;

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (!StatusManager.Instance.UseAttackBar())
            return;
        
        VFXHitPool.Instance.SpawnHitbox(target.position, target.rotation, damage);    
    }
}
