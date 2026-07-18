using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class CursorToWorldScreen : MonoBehaviour
{
    [SerializeField] private float depth;
    
    public Transform target;
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Mouse.current.position.ReadValue();
        screenPosition.z = _cam.nearClipPlane +  depth;
        
        worldPosition = _cam.ScreenToWorldPoint(screenPosition);
        
        transform.position = worldPosition;
    }
}
