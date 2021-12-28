using UnityEngine;

public class ItemShopPickUpInput : ItemPickUpProperties
{
    public Item item;

    bool addItem = false;

    PlayerStats stats;

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
        if(player != null)
        {
            stats = player.GetComponent<PlayerStats>();
            if(Input.GetKeyDown(KeyCode.E) && addItem == true && stats.coins >= item.value && stats != null)
            {
                stats.CurrencySubtract(item.value);
                PlayerInventory.instance.AddItem(item);
                Debug.Log("Picked up " + item.name);
                addItem = false;
                Destroy(gameObject);
            }else if(stats == null)
            {
                Debug.LogError(player.name + " does not have a PlayerStats.cs!");
            }
        }
    }
}
