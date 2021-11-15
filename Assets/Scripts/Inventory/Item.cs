using UnityEngine;

//Make it as a creatable object inside the project 
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public bool isDefaultItem = false;
    public Sprite icon = null;

    [Header("Item Parameters")]
    public itemClass itemType;
    public float Amount = 0f;

    //[]Make a selectable dropdown that determines the type of item
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        PlayerInventory.instance.RemoveItem(this);
    }
}

public enum itemClass { health, poison, movement}
