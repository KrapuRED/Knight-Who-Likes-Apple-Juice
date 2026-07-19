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
    public void PlayAnimation(Vector3 targetPosition, Action onComplete = null)
    {
        if (_isActive) return;

        _isActive = true;
        _moveAnim = StartCoroutine(MoveAnimation(targetPosition, onComplete));
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
