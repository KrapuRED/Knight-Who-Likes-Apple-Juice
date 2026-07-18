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


    public void IdleAnimation()
    {
        Debug.Log($"{gameObject.name}: IdleAnimation called");
        
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
    }
}
