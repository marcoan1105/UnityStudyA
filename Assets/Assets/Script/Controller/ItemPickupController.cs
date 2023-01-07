using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupController : InteractableController
{
    [SerializeField]
    Item Item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up item " + Item.Name);
        var wasPickedUp = InventoryController.Instance.Add(Item);

        if(wasPickedUp)
            Destroy(gameObject);
        
    }
}
