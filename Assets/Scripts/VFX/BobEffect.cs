using UnityEngine;

public class BobEffect : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float bobHeight = 0.2f;
    [SerializeField] private float bobSpeed = 2f;

    private Vector3 _startLocalPos;
    [SerializeField] private bool _isBobbing = true;

    private void Start()
    {
        _startLocalPos = parent.localPosition;
    }

    private void Update()
    {
        if (!_isBobbing)
            return;

        Debug.Log($"Bob position: {_startLocalPos}");
        float offset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        parent.localPosition = _startLocalPos + new Vector3(0f, offset, 0f);
    }

    public void StopBobbing()
    {
        _isBobbing = false;
        parent.localPosition = _startLocalPos; // snap back to rest position, avoid freezing mid-bob
    }

    public void ResumeBobbing()
    {
        _isBobbing = true;
    }
}
