using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform weaponParent;

    PlayerInventory inventory;

    InventorySlot[] itemSlots;

    void Start()
    {
        inventory = PlayerInventory.instance;

        itemSlots = itemsParent.GetComponentsInChildren<InventorySlot>();

        inventory.onItemChangedCallBack += UpdateUI;
    }

    void UpdateUI()
    {
        Debug.Log("updating UI!");
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                itemSlots[i].Additem(inventory.items[i]);
            }
            else
            {
                itemSlots[i].ClearSlot();
            }
        }
    }
}
