using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItemUI : MonoBehaviour
{
    public ItemInfo itemInfo;

    [SerializeField] GameObject itemInfoPanel;
    [SerializeField] TMP_Text itemNameText;

    public void ShowInfoPanel(){
        if(itemInfo == null) return; 

        itemNameText.text = itemInfo.name;
        itemInfoPanel.SetActive(true);
    }

    public void HideInfoPanel(){
        itemInfoPanel.SetActive(false);
    }

    public void EquipItem(){
        if(itemInfo == null) return;
        Inventory inventory = GetComponentInParent<Inventory>();

        if(itemInfo.equipSlot == EquipSlot.WEAPON){
            
        }
        else{
            if(inventory.equippedItems[itemInfo.equipSlot] != null){
                inventory.UnequipArmour(itemInfo.equipSlot);
            }
        }

        inventory.EquipItem(itemInfo);
    }

    public void UnequipItem(){
        if(itemInfo == null) return;
        Inventory inventory = GetComponentInParent<Inventory>();
        inventory.UnequipItem(itemInfo);
    }
}
