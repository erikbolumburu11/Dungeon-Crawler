using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DropTableEntry {
    public ItemInfo item;
    public float chance;
}

public class CharacterDropTable : MonoBehaviour
{
    [SerializeField] public List<DropTableEntry> dropTable; // {Item, Chance}
    [SerializeField] WorldItemDescription itemDropPrefab;

    public void SelectAndSpawnDrops(){
        foreach (DropTableEntry drop in dropTable)
        {
            float rand = UnityEngine.Random.Range(0.0f, 1.0f);

            if(rand < drop.chance){
                InstantiateDrop(drop.item);
            }
        }
    }

    void InstantiateDrop(ItemInfo item){
        WorldItemDescription drop = Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        drop.SetInfo(item);
    }
}