using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] currentEquipment;
    public Weapon currentWeapon;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChangedCallback;
    public delegate void OnWeaponChanged(Weapon newWeapon, Weapon oldWeapon);
    public OnWeaponChanged onWeaponChangedCallback;
    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //Gets an array of strings of equipment slots, then saves the amount of slots to numSlots.

        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        Debug.LogWarning("Equipping " + newItem.name);
        int slotIndex = (int)newItem.equipSlot;     //Finds the index of whichever slot this item is assigned to (using the enum) and saves it to slotIndex.

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)    //If the equip slot has something in it
        {
            oldItem = currentEquipment[slotIndex];  
            inventory.Add(oldItem);                 //Move that item back into the inventory
        }

        if (onEquipmentChangedCallback != null)     //If any methods need to be notified of an equipment change
        {
            onEquipmentChangedCallback.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        Weapon oldWeapon = null;
        if (currentWeapon != null)
        {
            oldWeapon = currentWeapon;
            inventory.Add(oldWeapon);
        }
        if (onWeaponChangedCallback != null)     //If any methods need to be notified of an equipment change
        {
            onWeaponChangedCallback.Invoke(newWeapon, oldWeapon);
        }

        currentWeapon = newWeapon;
    }

    public void UnequipEquipment(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)                    //If there is an item in this slot
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);                                 //Add it back to the inventory

            currentEquipment[slotIndex] = null;                     //Remove it from the slot

            if (onEquipmentChangedCallback != null)                 //If any methods need to be notified of an equipment change
            {
                onEquipmentChangedCallback.Invoke(null, oldItem);
            }
        }
    }
    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Weapon oldWeapon = currentWeapon;
            inventory.Add(oldWeapon);                                 //Add it back to the inventory

            currentWeapon = null;                     //Remove it from the slot

            if (onWeaponChangedCallback != null)                 //If any methods need to be notified of an equipment change
            {
                onWeaponChangedCallback.Invoke(null, oldWeapon);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            UnequipEquipment(i);
        }
        UnequipWeapon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
