using UnityEngine;
using UnityEngine.UI;

public abstract class StatusBarUI : MonoBehaviour
{
    public Slider sliderBar;
    
    public abstract void UpdateStatusBar(float amountCurrent, float amountMax);
}
