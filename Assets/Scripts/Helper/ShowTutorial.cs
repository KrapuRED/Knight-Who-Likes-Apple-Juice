using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PanelManager.Instance.OpenPanel("Panel - Tutorial");
    }
}
