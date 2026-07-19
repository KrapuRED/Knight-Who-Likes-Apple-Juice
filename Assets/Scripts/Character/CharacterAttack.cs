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

        if (ownerCharacter.IsHealing)
            return;

        ownerCharacter.CharacterAnimation.AttackAnimation(new[] { PointDirection.Center });

        VFXHitPool.Instance.SpawnHitbox(target.position, target.rotation, damage);
    }


    public void OnAttackByState(PointMovement currentPoint)
    {
        if (enemyCharacter != null && !enemyCharacter.IsAttackBarFull())
        {
            return;
        }
        
        Debug.Log($"Attack pos {currentPoint.gameObject.name}");
        ManagerPosition.Instance.DropHitBoxByPoint(currentPoint, enemyCharacter, enemyCharacter.DamageValue, currentPoint.PointDirection, 0);
        enemyCharacter.ResetCondition();
    }
    
    public void OnAttackMultipleByState(int attackIndex)
    {
        if (enemyCharacter != null && !enemyCharacter.IsAttackBarFull())
        {
            return;
        }
        
        foreach (PointMovement point in ManagerPosition.Instance.GetMultiplePointMovements())
            ManagerPosition.Instance.DropHitBoxByPoint(point, enemyCharacter, enemyCharacter.DamageValue, point.PointDirection, attackIndex);
        
        enemyCharacter.ResetCondition();
    }
}
