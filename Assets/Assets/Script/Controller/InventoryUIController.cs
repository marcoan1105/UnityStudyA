using Assets.Assets.Script.Configuration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{

    [SerializeField]
    GameObject InventoryUI;

    [SerializeField]
    Transform itemsParent;

    InventorySlotController[] slots;

    InventoryController Inventory;

    // Start is called before the first frame update
    void Start()
    {
        Inventory = InventoryController.Instance;
        Inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlotController>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(ButtonConfiguration.Inventory))
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {

        int itemsCount = Inventory.GetItems().Count;
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < itemsCount)
            {
                slots[i].AddItem(Inventory.GetItems()[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
