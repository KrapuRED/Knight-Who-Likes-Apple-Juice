using UnityEngine;

public class EnemyCharacter : Character, IDamageable
{
    public void TakeDamage(float damageValue)
    {
        Debug.Log($"{gameObject.name} is taking damage {damageValue}");
        CharacterHealth.UpdateHealthTakeDamage(damageValue);
    }
}
