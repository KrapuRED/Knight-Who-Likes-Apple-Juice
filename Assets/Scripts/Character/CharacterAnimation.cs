using System.Collections;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private BobEffect  bobEffect;

    [Header(("Sprite"))]
    [SerializeField] private Sprite idleSprites;
    [SerializeField] private Sprite dodgeSprites;
    [SerializeField] private Sprite healSprites;
    [SerializeField] private Sprite attackSprites;
    [SerializeField] private Sprite anticipationSprite;


    public void IdleAnimation()
    {
        Debug.Log($"{gameObject.name}: IdleAnimation called");
        
        bobEffect.ResumeBobbing();
        spriteRenderer.sprite = idleSprites;
    }
    
    public void DodgeAnimation(PointDirection direction)
    {
        Debug.Log($"{gameObject.name}: Dodge animation called {direction}");
        
        if (direction == PointDirection.Left)
            spriteRenderer.flipX = true;
        else
        {
            spriteRenderer.flipX = false;
        }
        
        bobEffect.StopBobbing();
        spriteRenderer.sprite = dodgeSprites;
    }

    public void AttackAnimation()
    {
        StartCoroutine(DelayIdleAnimation());
        bobEffect.StopBobbing();
        spriteRenderer.sprite = attackSprites;
    }
    
    public void HealAnimation()
    {
        StartCoroutine(DelayIdleAnimation());
        
        bobEffect.StopBobbing();
        spriteRenderer.sprite = healSprites;
    }

    public void AnticipationAnimation()
    {
        StartCoroutine(DelayAttackAnimation());
        bobEffect.StopBobbing();
    }
    
    IEnumerator DelayIdleAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        IdleAnimation();
    }
    
    IEnumerator DelayAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        AttackAnimation();
    }
}
