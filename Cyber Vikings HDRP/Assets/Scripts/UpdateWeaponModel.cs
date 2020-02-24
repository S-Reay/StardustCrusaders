using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateWeaponModel : MonoBehaviour
{
    EquipmentManager equip;
    public GameObject[] modelArray;
    public int modelNumber = 99;

    private void Start()
    {
        equip = EquipmentManager.instance;
        equip.onWeaponChangedCallback += UpdateViewModel; //Calls function whenever a new item is added or removed
        ModelSwitch();

    }

    void UpdateViewModel(Weapon newWeapon, Weapon oldWeapon)
    {
        Debug.Log("Updating Model");
        if (newWeapon == null)
        {
            modelNumber = 99;
            ModelSwitch();
            return;
        }
        if (newWeapon.modelID != 999)
        {
            modelNumber = newWeapon.modelID;
            ModelSwitch();
        }
        else
        {
            Debug.LogError("Cannot find model for " + newWeapon.name);
        }
    }

    void ModelSwitch()
    {
        for (int x = 0; x < modelArray.Length; x++)
        {
            if (x == modelNumber)
            {
                modelArray[x].SetActive(true);
            }
            else
            {
                modelArray[x].SetActive(false);
            }
        }
        modelNumber += 1;
        if (modelNumber > modelArray.Length - 1)
        {
            modelNumber = 0;
        }
    }
}
