using System;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public ItemSO itemDataSO;
    public int quality;
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance {get; private set;}

    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemUI itemUI;
    
    public ItemSO ItemDataSo => itemData.itemDataSO;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        itemUI.UpdateItemUI(itemData.quality);
    }

    public bool IsEnoughItem()
    {
        if (itemData.quality <= 0)
            return false;
        
        itemData.quality--;
        itemUI.UpdateItemUI(itemData.quality);

        return true;
    }
}
