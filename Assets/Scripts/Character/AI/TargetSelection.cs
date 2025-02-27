using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class TargetSelection : MonoBehaviour
{
    public static GameObject SelectTarget(Agent agent){
        List<GameObject> enemies = agent.GetVisibleEnemies();
        if(!enemies.IsNullOrEmpty())
            return agent.GetVisibleEnemies()[0];
        else
            return null;
    }
}