using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterItem : MonoBehaviour
{
    [SerializeField] private Character owenCharacter;
    
    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (ItemManager.Instance.IsEnoughItem())
            {
                ItemSO itemData = ItemManager.Instance.ItemDataSo;
                
                Debug.Log($"Using {itemData.itemName} that healing {itemData.itemEffect}%");
                owenCharacter.CharacterHealth.UpdateHealthRestore(itemData.itemEffect);
            }
        }
    }
}
