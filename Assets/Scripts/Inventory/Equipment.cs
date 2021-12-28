using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Inventory/Equipment")]
public class Equipment : ScriptableObject
{
    new public string name = "New Equipment";
    public bool isDefaultItem = false;
    public Sprite icon = null;

    [Header("Equipment Properties")]
    public EquipementClass equipementClass;
    public float damageModifier = 0f;
    public float armorModifier = 0f;
    public float movementModifier = 0f;
    public float value = 0f;
}
public enum EquipementClass { ChestPlateOrBracelet, Sandles }