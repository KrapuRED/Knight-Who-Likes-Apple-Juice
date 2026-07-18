using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text quantityText;
    
    public void UpdateItemUI(int quantity)
    {
        quantityText.text = quantity.ToString();
    }

}
