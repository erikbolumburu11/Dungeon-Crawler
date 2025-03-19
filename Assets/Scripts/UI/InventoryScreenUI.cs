using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreenUI : MonoBehaviour
{
    [SerializeField] GameObject itemUIElementPrefab;
    [SerializeField] Color emptyItemSlotColor;
    Inventory inventory;

    List<GameObject> instantiatedItemUIElements;

    void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        instantiatedItemUIElements = new();
        for (int i = 0; i < inventory.inventorySize; i++)
        {
            instantiatedItemUIElements.Add(Instantiate(itemUIElementPrefab, transform));
        }
        UpdateInventoryScreen();
    }

    void Update()
    {
        UpdateInventoryScreen();
    }

    public void UpdateInventoryScreen()
    {
        for (int i = 0; i < inventory.inventorySize; i++)
        {
            Image image = instantiatedItemUIElements[i].GetComponent<Image>();
            if(i < inventory.items.Count){
                image.sprite = inventory.items[i].image;
                image.color = Color.white;
                instantiatedItemUIElements[i].GetComponent<InventoryItemUI>().itemInfo = inventory.items[i];
            }
            else{
                image.sprite = null;
                image.color = emptyItemSlotColor;
            }
        }
    }
}