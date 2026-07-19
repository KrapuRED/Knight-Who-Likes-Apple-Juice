using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Character ownerCharacter;
    [SerializeField] private EnemyCharacter enemyCharacter;
    [SerializeField] private float damage;
    [SerializeField] private Transform target;

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (!StatusManager.Instance.UseAttackBar())
            return;
        
        ownerCharacter.CharacterAnimation.AttackAnimation(PointDirection.Center);
        
        VFXHitPool.Instance.SpawnHitbox(target.position, target.rotation, damage);    
    }


    public void OnAttackByState(PointMovement currentPoint)
    {
        if (enemyCharacter != null && !enemyCharacter.IsAttackBarFull())
        {
            return;
        }
        
        Debug.Log($"Attack pos {currentPoint.gameObject.name}");
        ManagerPosition.Instance.DropHitBoxByPoint(currentPoint, enemyCharacter, enemyCharacter.DamageValue, currentPoint.PointDirection);
        enemyCharacter.ResetCondition();
    }
    
    public void OnAttackMultipleByState()
    {
        if (enemyCharacter != null && !enemyCharacter.IsAttackBarFull())
        {
            return;
        }
        
        foreach (PointMovement point in ManagerPosition.Instance.GetMultiplePointMovements())
            ManagerPosition.Instance.DropHitBoxByPoint(point, enemyCharacter, enemyCharacter.DamageValue, point.PointDirection);
        
        enemyCharacter.ResetCondition();
    }
}
