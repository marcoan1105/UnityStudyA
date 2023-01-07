using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventorySlotController : MonoBehaviour
{   

    [SerializeField]
    Image icon;

    [SerializeField]
    Item item;

    [SerializeField]
    Button removeButton;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.Icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        InventoryController.Instance.Remove(item);
    }


    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
