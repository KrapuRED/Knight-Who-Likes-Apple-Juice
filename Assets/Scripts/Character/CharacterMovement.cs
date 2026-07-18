using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform pointContainer;
    [SerializeField] private List<PointMovement> points = new();

    [SerializeField] private PointMovement _currentPoint;
    private Coroutine _moveRoutine;
    
    private void Awake()
    {
        for (int i = 0; i < pointContainer.childCount; i++)
            points.Add(pointContainer.GetChild(i).GetComponent<PointMovement>());
    }

    private void Start()
    {
        foreach (PointMovement point in points)
            if (point.PointDirection == PointDirection.Center)
                _currentPoint = point;
        
        transform.position = _currentPoint.transform.position;
        
        Debug.Log($"{gameObject.name} is moving to the {_currentPoint.PointDirection}");
    }

    private void OnMove(Vector2 dir)
    {
        PointDirection direction = dir.x > 0 ? PointDirection.Right: PointDirection.Left;
        MoveToDirection(direction);
    }
    
    private void MoveToDirection(PointDirection targetDirection)
    {
        int currentIndex = (int)_currentPoint.PointDirection;
        int targetIndex = (int)targetDirection;

        if (currentIndex == targetIndex) return;
        
        int step = targetIndex > currentIndex ? 1 : -1;
        
        int nextIndex = currentIndex + step;
        
        PointMovement nextPoint = points.FirstOrDefault(p => (int)p.PointDirection == nextIndex);
    
        if (nextPoint == null) return;
        Debug.Log($"{gameObject.name} next stop at {nextPoint}");

        _currentPoint = nextPoint;

        if (_moveRoutine != null)
        {
            _moveRoutine = null;
            StopCoroutine(_moveRoutine);
        }

        _moveRoutine = StartCoroutine(MoveAlongPath(nextPoint, targetDirection));
    }

    private IEnumerator MoveAlongPath(PointMovement target, PointDirection direction)
    {
        while (Vector3.Distance(transform.position, target.transform.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target.transform.position;
        Debug.Log($"{gameObject.name} arrived at {target.PointDirection}");
            
        _moveRoutine = null; 
    }
    
    public void OnMovementInput(InputAction.CallbackContext context)
    { 
        Vector2 dir =  context.ReadValue<Vector2>();
        
        if (context.performed)
        {
            OnMove(dir);
        }
    }
}
