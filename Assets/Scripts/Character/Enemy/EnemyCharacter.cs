using UnityEngine;

public class EnemyCharacter : Character, IDamageable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(float damageValue)
    {
        Debug.Log($"{gameObject.name} is taking damage {damageValue}");
        CharacterHealth.UpdateHealthTakeDamage(damageValue);
    }
}
