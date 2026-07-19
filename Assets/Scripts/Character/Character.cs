using System;
using UnityEngine;

public enum CharacterType
{
    Player, 
    Enemy,
    Boss
}

public class Character : MonoBehaviour
{ 
    [SerializeField] private CharacterType characterType;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CharacterAttack characterAttack;
    [SerializeField] private CharacterHealth characterHealth;
    [SerializeField] private CharacterAnimation characterAnimation;
    [SerializeField] protected bool isAttackBarFull;
    [SerializeField] private bool _isHealing;
    
    public CharacterMovement CharacterMovement => characterMovement;
    public CharacterAttack CharacterAttack => characterAttack;
    public CharacterHealth CharacterHealth => characterHealth;
    public  CharacterAnimation CharacterAnimation => characterAnimation;
    public CharacterType CharacterType => characterType;
    public bool IsHealing => _isHealing;
    public bool IsAttackBarFull => isAttackBarFull;
    
    private void Awake()
    {
        if (characterHealth == null)
            characterHealth = GetComponent<CharacterHealth>();
        
        if (characterAttack == null)
            characterAttack = GetComponent<CharacterAttack>();
    
        if (characterMovement == null)
            characterMovement = GetComponent<CharacterMovement>();
            
    }
    
    public void OnDeadCharacter()
    {
        if (characterType == CharacterType.Player)
            GameManager.Instance.FailGame();
        else
        {
            GameManager.Instance.RemoveCharacter(this);
        }
        
        Destroy(gameObject);
    }
    
    public void TakeHeal(bool isHealing) => _isHealing = isHealing;
}
