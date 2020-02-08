using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public Button itemButton;
    public Transform chestParent;

    public void UseItem()
    {
        if (item != null)
        {
            Debug.Log("Adding " + item.name + " to inventory");
            Inventory.instance.Add(item);
            chestParent.GetComponent<Chest>().Remove(item);
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
