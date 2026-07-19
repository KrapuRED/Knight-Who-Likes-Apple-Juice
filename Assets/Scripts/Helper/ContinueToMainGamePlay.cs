using System;
using UnityEngine;

public class ContinueToMainGamePlay : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.ContinueToMainGamePlay();
    }
}
