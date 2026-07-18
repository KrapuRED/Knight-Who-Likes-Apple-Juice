using UnityEngine;

[CreateAssetMenu(fileName = "IsRandomAttackTime", menuName = "State Machine/Enemy/Condtion/IsRandomAttackTime")]
public class IsRandomAttackTime : ConditionSO
{
    public override bool CheckCondition()
    {
        return true;
    }
}
