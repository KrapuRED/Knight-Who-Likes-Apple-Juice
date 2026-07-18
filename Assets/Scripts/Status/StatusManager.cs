using System;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public static StatusManager Instance { get; private set; }

    [Header("Stamina bar Config")]
    [SerializeField] private float maxStamina;
    [SerializeField] private float currentStamina;
    [SerializeField] private float rateStamina;
    
    [Header("Attack bar Config")]
    [SerializeField] private float maxAttackBar;
    [SerializeField] private float currentAttackBar;
    [SerializeField] private float rateAttackBar;
    
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
    }

    private void UpdateAttackBar()
    {
        if (currentAttackBar >= maxAttackBar)
            return;
        
        currentAttackBar += Time.deltaTime * rateAttackBar;
    }

    public void UseStamina(float amount)
    {
        
    }

    public void UseAttackBar(float amount)
    {
        
    }
}
