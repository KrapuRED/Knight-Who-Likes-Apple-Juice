using UnityEngine;
using System.Collections.Generic;

public class ManagerPosition : MonoBehaviour
{
    public static ManagerPosition Instance {get; private set;}

    [SerializeField] private PointMovement currPointPlayer;
    [SerializeField] private List<PointMovement> pointMovements = new();
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCurrentPlayerPosition(PointMovement currentPointPlayer)
    {
        currPointPlayer = currentPointPlayer;
    }

    public PointMovement GetRandomPoint()
    {
        int randomIndex = Random.Range(0, pointMovements.Count);
        
        return pointMovements[randomIndex];
    }

    public void DropHitBoxByPoint(PointMovement attackPoint)
    {
        bool foundMatch = false;

        foreach (var point in pointMovements)
        {
            if (attackPoint.PointDirection == point.PointDirection)
            {
                attackPoint.ActivateHitBox();
                foundMatch = true;
            }
        }

        if (!foundMatch)
            Debug.LogWarning($"No point found matching direction {attackPoint.PointDirection}! Check PointDirection values in pointMovements list.");
    }
    
    public Transform GetCurrentPlayer() => currPointPlayer.transform;
}
