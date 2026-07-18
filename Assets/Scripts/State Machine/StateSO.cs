using UnityEngine;

public abstract class StateSO : ScriptableObject
{
    public abstract void EnterState();

    public abstract void ExcuteState(Character character);

    public abstract void ExitState();
}
