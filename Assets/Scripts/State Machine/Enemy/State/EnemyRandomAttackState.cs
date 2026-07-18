using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRandomAttackState", menuName = "State Machine/Enemy/State/EnemyRandomAttackState")]

public class EnemyRandomAttackState : StateSO
{
    [SerializeField] private float minAttackTime;
    [SerializeField] private float maxAttackTime;

    private float _currTime;
    private float _targetTime;
    
    public override void EnterState()
    {
        // Take any random position that player can move or attack current player pos
    }

    public override void ExcuteState(Character character)
    {
        _currTime += Time.deltaTime;

        if (_currTime >= _targetTime)
        {
            var point = ManagerPosition.Instance.GetRandomPoint();
            
            character.CharacterAttack.OnAttackByState(point);
            
            ResetCondition();
        }
    }

    public override void ExitState()
    {
        
    }
    
    private void ResetCondition()
    {
        _currTime = 0f;
        _targetTime = Random.Range(minAttackTime, maxAttackTime);
    }
}
