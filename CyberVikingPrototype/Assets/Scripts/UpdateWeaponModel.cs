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
        equip.onEquipmentChangedCallback += UpdateViewModel; //Calls function whenever a new item is added or removed
        ModelSwitch();

    }

    void UpdateViewModel(Equipment newItem, Equipment oldItem)
    {
        Debug.Log("Updating Model");
        if (newItem.modelID != 999)
        {
            modelNumber = newItem.modelID;
            ModelSwitch();
        }
        else
        {
            Debug.LogError("Cannot find model for " + newItem.name);
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
