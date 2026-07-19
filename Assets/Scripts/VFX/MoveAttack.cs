using System;
using System.Collections;
using UnityEngine;

public class MoveAttack : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float moveSpeed;
    
    private bool _isActive;
    private Vector3 _startPos;
    private Coroutine _moveAnim;
    
    private void Start()
    {
        _startPos = parent.position;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    // MoveAttack.cs
    public void PlayAnimation(PointDirection direction, Action onComplete = null)
    {
        if (_isActive) return;

        PointMovement targetPoint = ManagerPosition.Instance.GetEnemyPointByDirection(direction);
        if (targetPoint == null)
        {
            Debug.LogWarning($"{gameObject.name}: no point found for direction {direction}");
            return;
        }

        _isActive = true;
        _moveAnim = StartCoroutine(MoveAnimation(targetPoint.transform.position, onComplete));
    }

    private IEnumerator MoveAnimation(Vector3 targetPosition, Action onComplete)
    {
        while (Vector3.Distance(parent.position, targetPosition) > 0.01f)
        {
            parent.position = Vector3.MoveTowards(parent.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(parent.position, _startPos) > 0.01f)
        {
            parent.position = Vector3.MoveTowards(parent.position, _startPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        _isActive = false;
        _moveAnim = null;
        onComplete?.Invoke();
    }
}
