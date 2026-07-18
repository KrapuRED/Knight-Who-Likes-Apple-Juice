using System;
using UnityEngine;

public class ProgressBarUI : StatusBarUI
{
    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        
        UpdateStatusBar(gameManager.ProgressLevel,  gameManager.MaxProgressLevel);
    }

    public override void UpdateStatusBar(float amountCurrent, float amountMax)
    {
        sliderBar.value = amountCurrent / amountMax;
    }
}
