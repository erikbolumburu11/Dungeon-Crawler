using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [TabGroup("Inventory")] public int inventorySize;
    [TabGroup("Inventory")] public List<ItemInfo> items;

    public Dictionary<EquipSlot, ItemInfo> equippedItems;

    CharacterWeapons weapons;

    void Awake()
    {
        items = new();
        equippedItems = new(){
            {EquipSlot.HELMET, null},
            {EquipSlot.CHEST, null},
            {EquipSlot.LEGS, null},
            {EquipSlot.BOOTS, null},
            {EquipSlot.WEAPON, null}
        };
        weapons = GetComponent<CharacterWeapons>();
    }

    public void AddItem(ItemInfo itemInfo){
        items.Add(itemInfo);
    }

    public void EquipItem(ItemInfo itemInfo){
        if(itemInfo is ArmourInfo armourInfo)
        {
            EquipArmour(armourInfo);
        }
        else if(itemInfo is EquippableWeaponInfo weaponInfo)
        {
            weapons.EquipWeapon(weaponInfo);
            items.Remove(itemInfo);
        }
        else{
            Debug.LogError("Not armour or equippable weapon!");
        }
    }

    public void UnequipItem(ItemInfo itemInfo){
        if(itemInfo is ArmourInfo armourInfo)
        {
            UnequipArmour(armourInfo.equipSlot);
        }
        else if(itemInfo is EquippableWeaponInfo weaponInfo)
        {
            weapons.EquipWeapon(weaponInfo);
            items.Remove(itemInfo);
        }
        else{
            Debug.LogError("Not armour or equippable weapon!");
        }
    }

    void EquipArmour(ArmourInfo armourInfo){
        equippedItems[armourInfo.equipSlot] = armourInfo;
        items.Remove(armourInfo);
    }

    public void UnequipArmour(EquipSlot equipSlot){
        items.Add(equippedItems[equipSlot]);
        equippedItems[equipSlot] = null;
    }
}