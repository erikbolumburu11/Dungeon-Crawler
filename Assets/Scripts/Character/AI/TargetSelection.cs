using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public struct HitEntry {
    public float time;
    public GameObject hitBy;

    public HitEntry(float time, GameObject hitBy){
        this.time = time;
        this.hitBy = hitBy;
    }
}

public class TargetSelection : MonoBehaviour
{
    public List<HitEntry> recentHitEntries;
    [SerializeField] float hitForgivenessTime; // How long until we remove a hit entry.

    public float allyDetectionRange; // For inheriting targets from allies

    TeamComponent teamComponent;


    void Awake()
    {
        recentHitEntries = new();
        teamComponent = GetComponent<TeamComponent>();
    }

    public GameObject SelectTarget(Agent agent){
        List<GameObject> enemies = agent.GetVisibleEnemies();
        CleanupHitEntries();


        // Sees enemy
        if(!enemies.IsNullOrEmpty()){
            return agent.GetVisibleEnemies()[0];
        }

        // Has been hit by enemy
        if(recentHitEntries.Count != 0){
            return recentHitEntries[recentHitEntries.Count - 1].hitBy;
        }

        // Ally sees enemy
        List<GameObject> allies = TeamComponent.GetOppositeTeamObjectList(teamComponent.team);
        allies = allies.Where(
            x => Vector2.Distance(transform.position, x.transform.position) <= allyDetectionRange
        ).ToList();
        if(allies.Count != 0){
            foreach (GameObject ally in allies)
            {
                if(ally.TryGetComponent(out Agent allyAgent)){
                    if(allyAgent.target != null){
                        return allyAgent.target;
                    }
                }
            }
        }

        // No target found
        return null;
    }

    void CleanupHitEntries(){
        List<HitEntry> hitsToRemove = new();
        foreach (HitEntry hit in recentHitEntries)
            if(Time.time - hit.time > hitForgivenessTime) hitsToRemove.Add(hit);
        foreach (HitEntry hit in hitsToRemove)
            recentHitEntries.Remove(hit);
    }

}