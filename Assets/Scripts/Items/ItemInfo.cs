using UnityEngine;

public enum EquipSlot {
    HELMET,
    CHEST,
    LEGS,
    BOOTS,
    WEAPON
}

[CreateAssetMenu(fileName = "Default Item Info", menuName = "Item Info")]
public class ItemInfo : ScriptableObject
{
    public new string name;
    public Sprite image;
    public EquipSlot equipSlot;
}
