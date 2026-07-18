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
    
    public void ActivateHitBox(Character ownerCharacter, float damage)
    {
        spaceHurtBox.ActivateHitBox(ownerCharacter, damage);
    }
}
