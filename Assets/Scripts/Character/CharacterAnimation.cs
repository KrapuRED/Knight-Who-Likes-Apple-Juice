using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class AttackAnimationSet
{
    public int attackId; // matches whatever ID the boss's attack data uses
    public Sprite anticipationSprite;
    public Sprite attackSprite;
    public GameObject vfx;
}

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private BobEffect  bobEffect;
    [SerializeField] private MoveAttack moveAttack;

    [Header(("Sprite"))]
    [SerializeField] private Sprite idleSprites;
    [SerializeField] private Sprite dodgeSprites;
    [SerializeField] private Sprite healSprites;
    
    [Header("Attack Variants")]
    [SerializeField] private AttackAnimationSet[] attackSets;
    
    [Header(("VFX"))]
    [SerializeField] private GameObject vfxAttack;
    
    private Coroutine _animationRoutine;
    private AttackAnimationSet _activeSet; // tracks which set's VFX is currently shown, so Idle knows what to hide

    private void OnDestroy()
    {
        StopAllCoroutines();
        _animationRoutine = null;
    }

    private void SetAnimationRoutine(IEnumerator routine)
    {
        if (_animationRoutine != null)
            StopCoroutine(_animationRoutine);

        _animationRoutine = routine != null ? StartCoroutine(routine) : null;
    }

    private AttackAnimationSet GetAttackSet(int attackId)
    {
        foreach (var set in attackSets)
            if (set.attackId == attackId)
                return set;

        Debug.LogWarning($"{gameObject.name}: no AttackAnimationSet found for attackId {attackId}");
        return null;
    }

    private Vector3 GetTargetPosition(PointDirection[] directions)
    {
        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach (PointDirection dir in directions)
        {
            PointMovement point = ManagerPosition.Instance.GetEnemyPointByDirection(dir);
            if (point == null) continue;

            sum += point.transform.position;
            count++;
        }

        return count > 0 ? sum / count : transform.position;
    }

    public void IdleAnimation()
    {
        if (bobEffect == null)
            return;

        if (_activeSet?.vfx != null)
            _activeSet.vfx.SetActive(false);

        _activeSet = null;

        bobEffect.ResumeBobbing();
        spriteRenderer.sprite = idleSprites;
    }

    public void DodgeAnimation(PointDirection direction)
    {
        spriteRenderer.flipX = direction == PointDirection.Left;

        bobEffect.StopBobbing();
        spriteRenderer.sprite = dodgeSprites;

        SetAnimationRoutine(null);
    }

    public void AttackAnimation(PointDirection[] directions, int attackId, string soundEffect)
    {
        AttackAnimationSet set = GetAttackSet(attackId);
        if (set == null || set.attackSprite == null)
            return;

        _activeSet = set;
        spriteRenderer.sprite = set.attackSprite;

        Vector3 spawnPos = GetTargetPosition(directions);

        if (set.vfx != null)
        {
            set.vfx.SetActive(true);
            set.vfx.transform.position = spawnPos;
        }
        
        if (!string.IsNullOrEmpty(soundEffect) && moveAttack == null)
            SoundEffectManager.Instance.PlaySound2D(soundEffect);
        
        if (moveAttack != null)
        {
            if (directions.Length == 1)
                moveAttack.PlayAnimationByPointDirection(directions[0], soundEffect, IdleAnimation);
            else
                moveAttack.PlayAnimationByVector(spawnPos, soundEffect, IdleAnimation);
        }
        else
        {
            SetAnimationRoutine(DelayIdleAnimation());
        }
    }

    public void AnticipationAnimation(PointDirection[] directions, int attackId, string soundEffect)
    {
        AttackAnimationSet set = GetAttackSet(attackId);
        if (set == null || set.anticipationSprite == null)
            return;

        spriteRenderer.sprite = set.anticipationSprite;

        if (bobEffect != null)
            bobEffect.StopBobbing();

        SetAnimationRoutine(DelayAttackAnimation(directions, attackId, soundEffect));
    }

    public void PlayHealAnimation()
    {
        bobEffect.StopBobbing();

        owner.TakeHeal(true);
        spriteRenderer.sprite = healSprites;

        SetAnimationRoutine(HealingAnimation());
    }

    private IEnumerator DelayAttackAnimation(PointDirection[] directions, int attackId, string soundEffect)
    {
        yield return new WaitForSeconds(.5f);
        AttackAnimation(directions, attackId, soundEffect);
    }

    private IEnumerator DelayIdleAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        IdleAnimation();
        _animationRoutine = null;
    }

    private IEnumerator HealingAnimation()
    {
        yield return new WaitForSeconds(1f);
        owner.TakeHeal(false);
        IdleAnimation();
        _animationRoutine = null;
    }
}
