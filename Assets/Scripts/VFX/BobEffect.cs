using UnityEngine;

public class BobEffect : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float bobHeight = 0.2f;
    [SerializeField] private float bobSpeed = 2f;

    private Vector3 _startPos;
    [SerializeField] private bool _isBobbing = true;

    private void Start()
    {
        _startPos = parent.position;
    }

    private void Update()
    {
        if (!_isBobbing)
            return;

        float offset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        parent.position = _startPos + new Vector3(0f, offset, 0f);
    }

    public void StopBobbing()
    {
        _isBobbing = false;
        _startPos = parent.position; // capture current position as the new rest point
    }

    public void ResumeBobbing()
    {
        _startPos = parent.position; // re-anchor to wherever the character actually is now
        _isBobbing = true;
    }
}
