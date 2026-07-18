using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ManagerPosition : MonoBehaviour
{
    public static ManagerPosition Instance {get; private set;}

    [SerializeField] private PointMovement currPointPlayer;
    [SerializeField] private List<PointMovement> pointMovements = new();
    
    public PointMovement CurrentPlayer => currPointPlayer;
    
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
                point.ActivateHitBox(); // activate the manager's own lane point, not the passed-in reference
                foundMatch = true;
            }
        }

        if (!foundMatch)
            Debug.LogWarning($"No point found matching direction {attackPoint.PointDirection}!");
    }
    
    public List<PointMovement> GetMultiplePointMovements()
    {
        List<PointMovement> result = new();

        PointMovement centerPoint = pointMovements.FirstOrDefault(p => p.PointDirection == PointDirection.Center);
        if (centerPoint != null)
            result.Add(centerPoint);

        PointDirection sideDirection = Random.value < 0.5f ? PointDirection.Left : PointDirection.Right;
        PointMovement sidePoint = pointMovements.FirstOrDefault(p => p.PointDirection == sideDirection);
        if (sidePoint != null)
            result.Add(sidePoint);

        return result;
    }
}
