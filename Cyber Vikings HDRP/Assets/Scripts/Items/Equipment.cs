using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armourModifier;
    public int damageModifier;
    public int moveSpeedModifier;
    public int jumpModifier;

    public int modelID = 999; //Set to 999 as default to allow non weapon items to not affect model

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Arms, R_Hand, L_Hand, Eyes, Feet}
