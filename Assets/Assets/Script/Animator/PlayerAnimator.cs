using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> WeaponAnimationDict;

    protected override void Start()
    {
        base.Start();

        EquipmentController.Instance.onEquipmentChanged += OnEquipmentChanged;

        WeaponAnimationDict = new Dictionary<Equipment, AnimationClip[]>();

        foreach (WeaponAnimations a in weaponAnimations)
        {
            WeaponAnimationDict.Add(a.weapon, a.clips);
        }
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (WeaponAnimationDict.ContainsKey(newItem))
            {
                CurrentAttackAnimSet = WeaponAnimationDict[newItem];
            }
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
            if (WeaponAnimationDict.ContainsKey(oldItem))
            {
                CurrentAttackAnimSet = DefaultAttackAnimSet;
            }
        }

        if (newItem != null && newItem.equipSlot == EquipentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1);
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipentSlot.Shield)
        {
            animator.SetLayerWeight(2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
