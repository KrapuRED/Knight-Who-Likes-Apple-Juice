using System;
using System.Collections;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private BobEffect  bobEffect;
    [SerializeField] private MoveAttack moveAttack;

    [Header(("Sprite"))]
    [SerializeField] private Sprite idleSprites;
    [SerializeField] private Sprite dodgeSprites;
    [SerializeField] private Sprite healSprites;
    [SerializeField] private Sprite attackSprites;
    [SerializeField] private Sprite anticipationSprite;

    private Coroutine _animationRoutine;
    
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void SetAnimationRoutine(IEnumerator routine)
    {
        if (_animationRoutine != null)
            StopCoroutine(_animationRoutine);

        _animationRoutine = routine != null ? StartCoroutine(routine) : null;
    }
    
    public void IdleAnimation()
    {
        bobEffect.ResumeBobbing();
        spriteRenderer.sprite = idleSprites;
    }
    
    public void DodgeAnimation(PointDirection direction)
    {
        if (direction == PointDirection.Left)
            spriteRenderer.flipX = true;
        else
        {
            spriteRenderer.flipX = false;
        }
        
        bobEffect.StopBobbing();
        spriteRenderer.sprite = dodgeSprites;
        
        SetAnimationRoutine(null);
    }

    public void AttackAnimation(PointDirection direction)
    {
        spriteRenderer.sprite = attackSprites;

        if (moveAttack != null)
            moveAttack.PlayAnimation(direction, IdleAnimation); // idle (and ResumeBobbing) fires exactly when lunge finishes
        else
            SetAnimationRoutine(DelayIdleAnimation()); // fallback for enemies without a lunge
    }
    
    public void HealAnimation()
    {
        bobEffect.StopBobbing();
        spriteRenderer.sprite = healSprites;
        
        SetAnimationRoutine(DelayIdleAnimation());
    }

    public void AnticipationAnimation(PointDirection direction)
    {
        bobEffect.StopBobbing();
        
        SetAnimationRoutine(DelayAttackAnimation(direction));
    }
    
    IEnumerator DelayIdleAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        IdleAnimation();
        _animationRoutine = null;
    }
    
    IEnumerator DelayAttackAnimation(PointDirection direction)
    {
        yield return new WaitForSeconds(0.5f);
        AttackAnimation(direction);
    }
}
