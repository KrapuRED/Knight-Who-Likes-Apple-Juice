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
        characterMovement = GetComponent<CharacterMovement>();
        characterAttack   = GetComponent<CharacterAttack>();
        characterHealth   = GetComponent<CharacterHealth>();
    }
    
    public void OnDeadCharacter()
    {
        Destroy(gameObject);
    }
}
