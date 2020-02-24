using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment
{
    public bool isTwoHanded;
    public RangedAttackType rangedAttack;
    public int modelID = 999; //Set to 999 as default to allow non weapon items to not affect model


    public override void Use()
    {
        EquipmentManager.instance.EquipWeapon(this);
        RemoveFromInventory();
    }
}
public enum RangedAttackType { Raycast, Slash, Grenade }
