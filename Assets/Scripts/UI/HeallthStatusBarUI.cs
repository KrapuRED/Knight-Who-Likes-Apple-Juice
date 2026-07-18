using UnityEngine;

public class HeallthStatusBarUI : StatusBarUI
{
    public override void UpdateStatusBar(float amountCurrent, float amountMax)
    {

        sliderBar.value = amountCurrent / amountMax;
    }
}
