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

    public bool IsEnoughItem()
    {
        if (itemData.quality <= 0)
            return false;
        
        itemData.quality--;
        return true;
    }
}
