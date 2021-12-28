using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpInput : ItemPickUpProperties
{
    public Item item;

    bool addItem = false;

    public override void InteractAction()
    {
        base.InteractAction();

        if (hasInteracted == true)
        {
            if (item != null && !item.isConsumableItem)
            {
                addItem = true;
            }
            else
            {
                Debug.LogError("No Item has been set");
                addItem = false;
            }
        }
        else
        {
            addItem = false;
        }
        
    }

    void LateUpdate()
    {
        PickUp();
    }

    void PickUp() 
    {
        if (Input.GetKeyDown(KeyCode.E) && addItem == true && isShopItem == false)
        {
            
            PlayerInventory.instance.AddItem(item);
            Debug.Log("Picked up " + item.name);
            addItem = false;
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.E) && addItem == true && isShopItem == true)
        {
            if (player.GetComponent<PlayerStats>().coins >= shopCost)
            {
                //take money, give item.
                player.GetComponent<PlayerStats>().CurrencySubtract(shopCost);

                PlayerInventory.instance.AddItem(item);
                Debug.Log("Picked up " + item.name);
                addItem = false;
                Destroy(gameObject);
            }
        }
    }
}
