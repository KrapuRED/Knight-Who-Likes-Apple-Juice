using UnityEngine;

public class StamineStatusBarUI : StatusBarUI
{
    public override void UpdateStatusBar(float amountCurrent, float amountMax)
    {

        sliderBar.value = amountCurrent / amountMax;
    }
}
