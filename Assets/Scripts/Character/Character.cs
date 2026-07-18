using System;
using UnityEngine;

public class Character : MonoBehaviour
{ 
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CharacterAttack characterAttack;
    [SerializeField] private CharacterHealth characterHealth;
    
    public CharacterMovement CharacterMovement => characterMovement;
    public CharacterAttack CharacterAttack => characterAttack;
    public CharacterHealth CharacterHealth => characterHealth;
    
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
