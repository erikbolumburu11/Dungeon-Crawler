using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int inventorySize;
    public List<ItemInfo> items;

    void Awake()
    {
        items = new();
    }

    public void AddItem(ItemInfo itemInfo){
        items.Add(itemInfo);
    }
}
