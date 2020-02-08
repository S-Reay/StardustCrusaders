using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public List<Item> items = new List<Item>();
    public int capacity = 20;
    public GameObject UI;
    public GameObject itemsParent;
    GameObject player;

    ChestSlot[] slots;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slots = itemsParent.GetComponentsInChildren<ChestSlot>();
        UpdateUI();
    }

    public override void Interact()
    {
        base.Interact();
        OpenChest();
    }

    public void OpenChest()
    {
        Debug.Log("Chest: " + transform.name + " has been opened");
        UI.SetActive(true);
        //ALLOW PLAYER TO CLICK ITEMS TO ADD TO INVENTORY
        //ALLOW PLAYER TO CLOSE CHEST
    }

    private void Update()
    {
        if (UI.activeSelf)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 5f)
            {
                UI.SetActive(false);
            }
        }
    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].AddItem(items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        UpdateUI();
    }
}
