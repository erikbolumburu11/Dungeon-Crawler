using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemSlotUI : InventoryItemUI 
{
    public EquipSlot equipSlot;

    Inventory inventory;
    Image image;

    Color emptyEquipSlotColor;

    void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        image = GetComponent<Image>();
        emptyEquipSlotColor = image.color;
    }

    void Update()
    {
        SetDisplayInfo(inventory.equippedItems[equipSlot]);
    }

    void SetDisplayInfo(ItemInfo equippedItem){
        if(equippedItem == null){
            ResetDisplayInfo();
            return;
        }
        image.sprite = equippedItem.image;
        image.color = Color.white;
        itemInfo = equippedItem;
    }

    void ResetDisplayInfo(){
        image.sprite = null;
        image.color = emptyEquipSlotColor;
        itemInfo = null;
    }
}
