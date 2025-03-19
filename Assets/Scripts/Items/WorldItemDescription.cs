using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldItemDescription : MonoBehaviour
{
    public ItemInfo itemInfo;

    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text itemNameText;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out PlayerBrain playerBrain)){
            panel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.TryGetComponent(out PlayerBrain playerBrain)){
            panel.SetActive(false);
        }
    }

    public void SetInfo(ItemInfo itemInfo){
        itemNameText.text = itemInfo.name;
        this.itemInfo = itemInfo;
        GetComponent<SpriteRenderer>().sprite = itemInfo.image;
    }
}
