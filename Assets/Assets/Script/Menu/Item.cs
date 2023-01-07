using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string Name = "New Item";
    public Sprite Icon = null;
    public bool isDefaultItem = false;

    public SkinnedMeshRenderer mesh;

    public virtual void Use()
    {

        Debug.Log("Using " + name);
    }

    public virtual void RemoveFromInventory()
    {
        InventoryController.Instance.Remove(this);
    }
}
