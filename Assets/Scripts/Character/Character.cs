using System;
using UnityEngine;

public enum CharacterType
{
    Player, 
    Enemy
}

public class Character : MonoBehaviour
{ 
    [SerializeField] private CharacterType characterType;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CharacterAttack characterAttack;
    [SerializeField] private CharacterHealth characterHealth;
    [SerializeField] private CharacterAnimation characterAnimation;
    
    public CharacterMovement CharacterMovement => characterMovement;
    public CharacterAttack CharacterAttack => characterAttack;
    public CharacterHealth CharacterHealth => characterHealth;
    public  CharacterAnimation CharacterAnimation => characterAnimation;
    public CharacterType CharacterType => characterType;
    
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
        Destroy(gameObject);
    }
}
