using UnityEngine;

[System.Serializable]
public enum PointDirection
{
    Left,
    Center,
    Right
}

public class PointMovement : MonoBehaviour
{
    [SerializeField] private PointDirection pointDirection;
    
    public PointDirection PointDirection => pointDirection;
    
    [SerializeField] private SpaceHurtBox spaceHurtBox;
    
    public float ActivateHitBox(Character owner, float damage, PointDirection direction, int indexAttack)
    {
        return spaceHurtBox.ActivateHitBox(owner, damage, direction, indexAttack);
    }
}
