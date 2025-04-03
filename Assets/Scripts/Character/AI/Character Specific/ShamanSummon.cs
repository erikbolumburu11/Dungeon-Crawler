using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanSummon : MonoBehaviour
{
    public int maxSummonedAllyCount;
    List<GameObject> summonedAllies;

    public float summonCooldown;
    bool readyToSummon = true;

    [SerializeField] GameObject allyToSpawnPrefab;

    void Awake()
    {
        summonedAllies = new();
    }

    void Update()
    {
        summonedAllies.RemoveAll(x => x == null); // Remove Nulls

        if(!CanSummon()) return;

        int enemiesToSpawnCount = maxSummonedAllyCount - summonedAllies.Count;
        for (int i = 0; i < enemiesToSpawnCount; i++)
        {
            GameObject ally = Instantiate(allyToSpawnPrefab, transform.position, Quaternion.identity);
            summonedAllies.Add(ally);
        }

        readyToSummon = false;
        ActionOnTimer.GetTimer(gameObject, "SummonTimer").SetTimer(summonCooldown, () => {
            readyToSummon = true;
        });
        
    }

    bool CanSummon(){
        if(!readyToSummon) return false;

        if(summonedAllies.Count <= maxSummonedAllyCount / 2) return true;
        
        return false;
    }
}
