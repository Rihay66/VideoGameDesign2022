using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPickUpInput : ItemPickUpProperties
{
    public Equipment equipment;

    bool addEquipment = false;

    public override void InteractAction()
    {
        base.InteractAction();

        if(hasInteracted == true)
        {
            if (equipment != null)
            {
                addEquipment = true;
            }
            else
            {
                Debug.LogError("No Equipment has been set");
                addEquipment = false;
            }
        }
        else
        {
            addEquipment = false;
        }
    }

    void LateUpdate()
    {
        PickUp();
    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E) && addEquipment == true && player != null && isShopItem == true)
        {
            PlayerInventory.instance.AddEquipment(equipment);
            Debug.Log("Picked up " + equipment.name);
            addEquipment = false;
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E) && addEquipment == true && player != null && isShopItem == true)
        {
            //take money, get item
            player.GetComponent<PlayerStats>().CurrencySubtract(shopCost);

            PlayerInventory.instance.AddEquipment(equipment);
            Debug.Log("Picked up " + equipment.name);
            addEquipment = false;
            Destroy(gameObject);
        }
    }
}
