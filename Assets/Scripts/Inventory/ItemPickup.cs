using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : ItemPickUpProperties
{
    public Item item;
    
    public override void InteractAction()
    {
        base.InteractAction();

        if(hasInteracted == true)
        {
            if (item != null && item.isConsumableItem)
            {
                PickUp();
            }
            
            else
            {
                Debug.LogError("No Item has been set");
            }
        }
    }

    void PickUp()
    {
        Debug.Log("Picked up " + item.name);
        int i = (int)item.itemType;
        if(i == 2)
        {
            PlayerInventory.instance.AddPlayerCoinage(item.value);
        }
        else if(i == 1)
        {
            PlayerInventory.instance.AddPlayerHealth(item.Amount);
        }
        Destroy(gameObject);
    }
}
