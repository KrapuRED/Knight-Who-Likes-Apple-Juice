using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
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
        
        SoundEffectManager.Instance.PlaySound2D("player_attack");
        ownerCharacter.CharacterAnimation.AttackAnimation(new[] { PointDirection.Center }, 0, string.Empty);

        VFXHitPool.Instance.SpawnHitbox(target.position, target.rotation, damage);
    }


    public void OnAttackByState(PointMovement currentPoint, string soundEffect)
    {
        if (enemyCharacter == null)
            return;
        
        Debug.Log("Attack!");

        float warningDelay = ManagerPosition.Instance.DropHitBoxByPoint(currentPoint, enemyCharacter, enemyCharacter.DamageValue, currentPoint.PointDirection, 0);
        StartCoroutine(DelayedAnticipation(new[] { currentPoint.PointDirection }, 0, warningDelay, soundEffect));

        enemyCharacter.ResetCondition();
    }

    public void OnAttackMultipleByState(int attackIndex, string soundEffect)
    {
        if (enemyCharacter == null)
            return;

        List<PointMovement> points = ManagerPosition.Instance.GetMultiplePointMovements();
        PointDirection[] directions = points.Select(p => p.PointDirection).ToArray();

        foreach (PointMovement point in points)
            ManagerPosition.Instance.DropHitBoxByPoint(point, enemyCharacter, enemyCharacter.DamageValue, point.PointDirection, attackIndex);

        StartCoroutine(DelayedAnticipation(directions, attackIndex, 2f, soundEffect));
        enemyCharacter.ResetCondition();
    }
    
    private IEnumerator DelayedAnticipation(PointDirection[] directions, int attackId, float delay, string soundEffect)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        enemyCharacter.CharacterAnimation.AnticipationAnimation(directions, attackId, soundEffect);
    }
}
