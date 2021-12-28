using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    #region Singleton
    public static PlayerInventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple instances of player inventory found!");
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallBack;

    public Text coinCounter;

    public int itemSpace = 2;
    public int equipmentSpace = 2;

    public List<Item> items;
    public List<Equipment> equipments;

    GameObject player;
    private PlayerStats stats;

    void Start()
    {
        items = new List<Item>(itemSpace);
        equipments = new List<Equipment>(equipmentSpace);
        for (int i = 0; i < itemSpace; i++)
            items.Add(null);
        for (int i = 0; i < equipmentSpace; i++)
            equipments.Add(null);

        Text coinCounter = GameObject.FindGameObjectWithTag("CoinCounter").GetComponent<Text>();

        if (PlayerManager.instance.playerInstance != null)
        {
            player = PlayerManager.instance.playerInstance;
            stats = player.GetComponent<PlayerStats>();
        }
        else
        {
            // If there isn't a PlayerManager it will give an error
            Debug.LogError(PlayerManager.instance.name + " Is either not found or its missing!");
            return;
        }
    }

    public bool AddItem(Item item)
    {
        if (!item.isConsumableItem)
        {
            int slot = (int)item.itemType;
            if (items[slot] != null)
            {
                items[slot] = item;

                print("Replaced item slot " + slot);

                if (onItemChangedCallBack != null)
                    onItemChangedCallBack.Invoke();

                return false;
            }else if(items[slot] == null)
            {
                items[slot] = item;
            }
            
            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }

        return true;
    }

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public bool AddEquipment(Equipment equipment)
    {
        if (!equipment.isDefaultItem)
        {
            int slot = (int)equipment.equipementClass;
            if (equipments[slot] != null)
            {
                //Removes previous equipments properties
                UnEquip(equipments[slot]);

                equipments[slot] = equipment;

                //Adds the new added equipments properties
                OnEquip(equipments[slot]);

                print("Replaced equipment slot " + slot);

                if (onItemChangedCallBack != null)
                    onItemChangedCallBack.Invoke();

                return false;
            }
            else if(equipments[slot] == null) 
            {
                equipments[slot] = equipment;
                OnEquip(equipment);
            }

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }

        return true;
    }

    public void RemoveEquipment(int index)
    {
        equipments.RemoveAt(index);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    void OnEquip(Equipment equipment)
    {
        if(equipment != null)
        {
            ModifyPlayerParameters(equipment.damageModifier, equipment.movementModifier, equipment.armorModifier);
        }
    }

    void UnEquip(Equipment equipment)
    {
        if(equipment != null)
        {
            ModifyPlayerParameters(-equipment.damageModifier, -equipment.movementModifier, -equipment.armorModifier);
        }
    }

    //[] Add in jump boost
    //[] Add a timer for items to remove their effects
    void UseItem(int index)
    {
        Item item = items[index];
        if (index == 0)
        {
            //Top item is for health items
            print("Using Health Item!");
            AddPlayerHealth(item.Amount);
            items[index] = null;
        }
        else if (index == 1)
        {
            //Bottom item is for temporary movement boost items
            //[] Make a temporary addition of movement
            print("using Movement or jump Item");
            if(item.isJumpType == true)
            {
                AddPlayerJumpTemp(item.Amount, item.duration, item);
            }
            else
            {
                AddPlayerMovementTemp(item.Amount, item.duration, item);
            }
            items[index] = null;
        }
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }


    private void Update()
    {
        coinCounter.text = stats.coins.ToString("0");
        float inputSelection = Input.GetAxis("Item Selection");
        if(items.Count > 0)
        {
            if(inputSelection < 0 && items[0] != null)
            { 
                UseItem(0);
            }
            else if(inputSelection > 0 && items[1] != null)
            {                             
                UseItem(1);
            }
        }
    }

    public void AddPlayerHealth(float amount)
    {
        if (stats != null)
        {
            stats.RestoreHealth(amount);
        }
    }

    public void AddPlayerMovementTemp(float amount, float time, Item item)
    {
        if(stats != null && item.isJumpType == false)
        {
            stats.TemporaryModifyPlayerItemStats(amount, 0f, time);
        }
    }
    public void AddPlayerJumpTemp(float amount, float time, Item item)
    {
        if (stats != null && item.isJumpType == true)
        {
            stats.TemporaryModifyPlayerItemStats(0f, amount, time);
        }
    }

    public void AddPlayerCoinage(float amount)
    {
        if(stats != null)
        {
            stats.CurrencyAdd(amount);
        }
    }

    public void ModifyPlayerParameters(float damage, float movementSpeed, float armor)
    {
        if (stats != null)
        {
            stats.ModifyPlayerEquipmentStats(damage, movementSpeed, armor);
        }
    }
}