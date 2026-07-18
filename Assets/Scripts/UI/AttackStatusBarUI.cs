using UnityEngine;

public class AttackStatusBarUI : StatusBarUI
{
    public override void UpdateStatusBar(float amountCurrent, float amountMax)
    {

        sliderBar.value = amountCurrent / amountMax;
    }
}
