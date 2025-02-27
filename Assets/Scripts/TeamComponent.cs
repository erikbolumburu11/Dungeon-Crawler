using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
    NEUTRAL,
    FRIENDLY,
    ENEMY
}
public class TeamComponent : MonoBehaviour
{
    public static bool teamObjectListsInitialized = false;
    public static List<GameObject> friendlyObjects;
    public static List<GameObject> enemyObjects;

    public Team team;

    void Awake()
    {
        if(!teamObjectListsInitialized){
            friendlyObjects = new();
            enemyObjects = new();
            teamObjectListsInitialized = true;
        }
    }

    public static void AddToTeamObjectList(GameObject obj){
        if(obj.TryGetComponent(out TeamComponent teamComponent)){
            switch (teamComponent.team)
            {
                case Team.FRIENDLY:
                    friendlyObjects.Add(obj);
                    break;
                case Team.ENEMY:
                    enemyObjects.Add(obj);
                    break;
                default:
                    break;
            }
        }
    }

    public static void RemoveFromTeamObjectList(GameObject obj){
        if(obj.TryGetComponent(out TeamComponent teamComponent)){
            switch (teamComponent.team)
            {
                case Team.FRIENDLY:
                    friendlyObjects.Remove(obj);
                    break;
                case Team.ENEMY:
                    enemyObjects.Remove(obj);
                    break;
                default:
                    break;
            }
        }
    }

    public static List<GameObject> GetOppositeTeamObjectList(Team team){
        switch (team)
        {
            case Team.FRIENDLY:
                return enemyObjects;
            case Team.ENEMY:
                return friendlyObjects;
            default:
                return null;
        }
    }
}
