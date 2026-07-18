using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackState", menuName = "State Machine/Enemy/State/EnemyAttackState")]

public class EnemyAttackState : StateSO
{
    [SerializeField] private float minAttackTime;
    [SerializeField] private float maxAttackTime;

    private float _currTime;
    private float _targetTime;
    
    public override void EnterState()
    {
        // Take any random position that player can move or attack current player pos
    }

    public override void ExcuteState()
    {
        CheckAttackState();
    }

    public override void ExitState()
    {
        
    }

    private void CheckAttackState()
    {
        _currTime += Time.deltaTime;

        if (_currTime >= _targetTime)
        {
            var point = ManagerPosition.Instance.GetRandomPoint();
        
            Debug.Log($"Enemy attack : {point.gameObject.name}");
            ResetCondition();
        }
    }
    
    private void ResetCondition()
    {
        _currTime = 0f;
        _targetTime = Random.Range(minAttackTime, maxAttackTime);
    }
}
