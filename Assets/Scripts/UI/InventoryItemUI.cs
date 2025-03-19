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
}
