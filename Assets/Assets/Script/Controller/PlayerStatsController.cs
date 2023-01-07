using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : CharacterStatsController
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentController.Instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            Armor.AddModifier(newItem.armorModifier);
            Damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            Armor.RemoveModifier(oldItem.armorModifier);
            Damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();

        PlayerManager.Instance.KillPlayer();
        // Kill the player        
    }
}
