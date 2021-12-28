using UnityEngine;

public class EquipmentShopPickUpInput : ItemPickUpProperties
{
    public Equipment equipment;

    bool addEquipment = false;

    PlayerStats stats;

    public override void InteractAction()
    {
        base.InteractAction();

        if (hasInteracted == true)
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
        if(player != null)
        {
            stats = player.GetComponent<PlayerStats>();
            if (Input.GetKeyDown(KeyCode.E) && addEquipment == true && stats.coins >= equipment.value && stats != null)
            {
                stats.CurrencySubtract(equipment.value);
                PlayerInventory.instance.AddEquipment(equipment);
                Debug.Log("Picked up " + equipment.name);
                addEquipment = false;
                Destroy(gameObject);
            }
            else if(stats == null)
            {
                Debug.LogError(player.name + " does not have a PlayerStats.cs!");
            }
        }

    }
}
