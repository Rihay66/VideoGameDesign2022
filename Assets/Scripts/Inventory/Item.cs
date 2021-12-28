using UnityEngine;
using UnityEditor;

//Make it as a creatable object inside the project 
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public bool isConsumableItem = false;
    public Sprite icon = null;

    [Header("Item Parameters")]
    public itemClass itemType;
    public float Amount = 0f;
    //for the purposes of shops
    public float value = 0f;
    // for duration of its effects
    [HideInInspector]
    public float duration = 0f;
    [HideInInspector]
    public bool isJumpType = false;

    private void OnValidate()
    {
        CheckVariables();
#if(UNITY_EDITOR)
        if (showFloat.instance != null)
        {
            showFloat.instance.OnInspectorGUI();
        }
        else
        {
            return;
        }
#endif
    }

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        PlayerInventory.instance.RemoveItem((int)itemType);
    }

    void CheckVariables()
    {
        float i = duration;
        if (i == 0 && itemType == itemClass.movementOrJump)
        {
            Debug.LogWarning(this.name + " 's duration is set to 0");
            return;
        }
        if (Amount == 0f && itemType != itemClass.coins)
        {
            Debug.LogWarning(this.name + "'s amount are set to 0");
            return;
        }
        else if (!isConsumableItem || itemType == itemClass.coins)
        {
            if (value == 0f)
            {
                Debug.LogWarning(this.name + "'s value are set to 0");
                return;
            }
        }
    }
}

public enum itemClass { health, movementOrJump, coins}

#if(UNITY_EDITOR)
[CustomEditor(typeof(Item))]
public class showFloat : Editor
{
    public static showFloat instance;

    private void OnValidate()
    {
        if(instance != this)
        {
            instance = this;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Item item = (Item)target;

        if (item.itemType == itemClass.movementOrJump)
        {
            item.duration = EditorGUILayout.FloatField("Duration", item.duration);
            item.isJumpType = EditorGUILayout.Toggle("Is Jump Type", item.isJumpType);
        }
    }
}
#endif