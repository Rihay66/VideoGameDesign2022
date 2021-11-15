using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : ItemPickUpProperties
{
    public Item item;

    public override void InteractAction()
    {
        base.InteractAction();

        if(item != null)
        {
            PickUp();
        }
        else
        {
            Debug.LogError("No Item has been set");
        }
    }

    void PickUp()
    {
        Debug.Log("Picked up " + item.name);
        // Sub or add health
        Destroy(gameObject);
    }
}
