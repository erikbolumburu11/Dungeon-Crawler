using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Statistics {
    public int strength;
    public int defense;
    public int intelligence;
    public int agility;

    public static Statistics operator +(Statistics a, Statistics b){
        return new Statistics(){
            strength = a.strength + b.strength,
            defense = a.defense + b.defense,
            intelligence = a.intelligence + b.intelligence,
            agility = a.agility + b.agility
        };
    }
}

public class CharacterStatistics : MonoBehaviour
{
    [SerializeField] public Statistics baseStatistics;

    Inventory inventory;

    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public Statistics GetStatistics(){
        return baseStatistics + CalculateTotalArmourBonus();
    }

    Statistics CalculateTotalArmourBonus(){
        Statistics stats = new();

        if(inventory == null) return stats; 

        foreach (KeyValuePair<EquipSlot, ItemInfo> item in inventory.equippedItems)
        {
            if(item.Value == null) continue;

            if(item.Value is ArmourInfo armour){
                stats += armour.statisticBonuses;
            }
        }

        return stats;
    }
}