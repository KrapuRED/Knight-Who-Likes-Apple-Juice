using UnityEngine;

public abstract class StateSO : ScriptableObject
{
    public abstract void EnterState();

    public abstract void ExcuteState();

    public abstract void ExitState();
}
