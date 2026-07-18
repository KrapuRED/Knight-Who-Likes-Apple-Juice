using System;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance { get; private set; }

    [Header("Stamina bar Config")]
    [SerializeField] private float maxStamina;
    [SerializeField] private float currentStamina;
    [SerializeField] private float rateStamina;
    [SerializeField] private StatusBarUI staminaBarUI;
    
    [Header("Attack bar Config")]
    [SerializeField] private float maxAttackBar;
    [SerializeField] private float currentAttackBar;
    [SerializeField] private float rateAttackBar;
    [SerializeField] private StatusBarUI attackBarUI;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateStamina();
        UpdateAttackBar();
    }

    private void UpdateStamina()
    {
        if (currentStamina >=  maxStamina)
            return;
        
        currentStamina += Time.deltaTime * rateStamina;
        staminaBarUI.UpdateStatusBar(currentStamina, maxStamina);
    }

    private void UpdateAttackBar()
    {
        if (currentAttackBar >= maxAttackBar)
            return;
        
        currentAttackBar += Time.deltaTime * rateAttackBar;
        attackBarUI.UpdateStatusBar(currentAttackBar, maxAttackBar);
    }

    public bool UseStamina(float amount)
    {
        if (currentStamina < amount)
            return false;
    
        currentStamina -= amount;
        return true;
    }

    public bool UseAttackBar()
    {
        if (currentAttackBar < maxAttackBar)
            return false;
        
        currentAttackBar = 0;
        return true;
    }
}
