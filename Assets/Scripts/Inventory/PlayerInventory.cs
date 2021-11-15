using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int itemSpace = 2;
    public int equipmentSpace = 2;

    public List<Item> items = new List<Item>();
    public List<Item> equipment = new List<Item>();

    GameObject player;

    private PlayerStats stats;

    void Start()
    {
        if (PlayerManager.instance.playerInstance != null)
        {
            player = PlayerManager.instance.playerInstance;
            stats = player.GetComponent<PlayerStats>();
        }
        else
        {
            // If there isn't a PlayerManager it will give an error
            Debug.LogError(PlayerManager.instance.name + " Is either not found or its missing!");
        }
    }

    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= itemSpace)
            {
                Debug.Log(gameObject.name + " Not enough space!");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }

        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }

    public void AddPlayerHealth(float amount)
    {
        if(stats != null)
        {
            stats.RestoreHealth(amount);
        }
    }

    //[] Have a method that determines what the item will do by the selected class
    //[] Have a function that can use the number pad or directional arrows to use a item
}
