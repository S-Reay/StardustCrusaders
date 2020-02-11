using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    void Start()
    {
        EquipmentManager.instance.onEquipmentChangedCallback += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armour.AddModifier(newItem.armourModifier);
            damage.AddModifier(newItem.damageModifier);
            moveSpeed.AddModifier(newItem.moveSpeedModifier);
            jumpHeight.AddModifier(newItem.jumpModifier);
        }

        if (oldItem != null)
        {
            armour.RemoveModifier(oldItem.armourModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            moveSpeed.RemoveModifier(oldItem.moveSpeedModifier);
            jumpHeight.RemoveModifier(oldItem.jumpModifier);
        }
    }
}
