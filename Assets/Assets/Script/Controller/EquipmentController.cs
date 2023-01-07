using Assets.Assets.Script.Configuration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    #region Singleton
    public static EquipmentController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        Instance = this;
    }
    #endregion
    
    public Equipment[] CurrentEquipment;
    public Equipment[] DefaultItens;
    public SkinnedMeshRenderer targetMesh;
    public SkinnedMeshRenderer[] CurrentMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    InventoryController Inventory;

    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipentSlot)).Length;
        CurrentEquipment = new Equipment[numSlots];
        CurrentMeshes = new SkinnedMeshRenderer[numSlots];


        Inventory = InventoryController.Instance;

        EquipDefaultItens();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);

        CurrentEquipment[slotIndex] = newItem;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        CurrentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (CurrentEquipment[slotIndex] != null)
        {
            if(CurrentMeshes[slotIndex] != null)
            {
                Destroy(CurrentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = CurrentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);

            if (!oldItem.isDefaultItem)
            {
                Inventory.Add(oldItem);
            }
            
            CurrentEquipment[slotIndex] = null;
            CurrentMeshes[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }

        return null;
    }

    public void UnequipAll()
    {
        for(int i = 0; i < CurrentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItens();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions){
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItens()
    {
        foreach(Equipment item in DefaultItens)
        {
            Equip(item);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(ButtonConfiguration.UnequipAll))
        {
            UnequipAll();
        }
    }
}
