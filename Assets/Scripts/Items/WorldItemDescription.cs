using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldItemDescription : MonoBehaviour
{
    public ItemInfo itemInfo;

    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text itemNameText;
    [SerializeField] TMP_Text itemModifiersText;

    void Awake()
    {
        if(itemInfo != null) SetInfo(itemInfo);
    }

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
        if(itemInfo is ArmourInfo armour) itemModifiersText.text = GenerateArmourModifierText(armour);
        this.itemInfo = itemInfo;
        GetComponent<SpriteRenderer>().sprite = itemInfo.image;
    }

    public static string GenerateArmourModifierText(ArmourInfo armour){
        Statistics stats = armour.statisticBonuses;
        string text = ""; 

        if(stats.strength != 0) text += AddModifierColor($"Strength: {stats.strength}\n", stats.strength);
        if(stats.defense != 0) text += AddModifierColor($"Defense: {stats.defense}\n", stats.defense);
        if(stats.intelligence != 0) text += AddModifierColor($"Intelligence: {stats.intelligence}\n", stats.intelligence);
        if(stats.agility != 0) text += AddModifierColor($"Agility: {stats.agility}\n", stats.agility);

        return text;
    }

    public static string AddModifierColor(string text, int modifierValue){
        if(modifierValue > 0) return $"<color=green>{text}</color>";
        else return $"<color=red>{text}</color>";
    }
}
