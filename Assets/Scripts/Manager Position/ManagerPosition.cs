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
        currentPointPlayer = currentPointPlayer;
    }

    public PointMovement GetRandomPoint()
    {
        int randomIndex = Random.Range(0, pointMovements.Count);
        
        return pointMovements[randomIndex];
    }
    
    public Transform GetCurrentPlayer() => currPointPlayer.transform;
}
