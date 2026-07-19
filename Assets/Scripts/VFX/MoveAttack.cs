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
    public void PlayAnimationByPointDirection(PointDirection direction, string soundEffect,  Action onComplete)
    {
        if (_isActive) return;

        PointMovement targetPoint = ManagerPosition.Instance.GetEnemyPointByDirection(direction);
        if (targetPoint == null)
        {
            Debug.LogWarning($"{gameObject.name}: no point found for direction {direction}");
            return;
        }

        _isActive = true;
        _moveAnim = StartCoroutine(MoveAnimation(targetPoint.transform.position, onComplete, soundEffect));
    }

     public void PlayAnimationByVector(Vector3 target, string soundEffect, Action onComplete = null)
    {
        if (_isActive) return;

        _isActive = true;
        _moveAnim = StartCoroutine(MoveAnimation(target, onComplete, soundEffect));
    }

    private IEnumerator MoveAnimation(Vector3 targetPosition, Action onComplete, string soundEffect)
    {
        if (!string.IsNullOrEmpty(soundEffect))
            SoundEffectManager.Instance.PlaySound2D(soundEffect);
        
        while (Vector3.Distance(parent.position, targetPosition) > 0.1f)
        {
            parent.position = Vector3.MoveTowards(parent.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        while (Vector3.Distance(parent.position, _startPos) > 0.1f)
        {
            parent.position = Vector3.MoveTowards(parent.position, _startPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        _isActive = false;
        _moveAnim = null;
        onComplete?.Invoke();
    }
}
