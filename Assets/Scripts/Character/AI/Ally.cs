using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class Ally : Agent
{
    void Start() {
        tree = new BehaviourTree("Ally");

        PrioritySelector actions = new("Agent Logic");
        actions.AddChild(new Leaf("Follow", new FollowStrategy(this), 0));

        Sequence isInCombat = new Sequence("InCombat?", 10);

        isInCombat.AddChild(new Leaf(
            "InCombatCondition",
            new Condition(IsInCombat)
        ));

        Selector inCombatDecision = new Selector("Move To Enemy or Last Seen Pos");

        Sequence enemyVisible = new Sequence("EnemyVisible?", 10);
        enemyVisible.AddChild(new Leaf(
            "EnemyVisibleCondition",
            new Condition(() => {return !GetVisibleEnemies().IsNullOrEmpty(); })
        ));
        enemyVisible.AddChild(new Leaf("MoveToEnemy", new MoveToAttackStrategy(this)));
        inCombatDecision.AddChild(enemyVisible);

        inCombatDecision.AddChild(new Leaf(
            "MoveToEnemyLastSeenPos",
            new MoveToEnemyLastSeenPos(this)
        ));


        isInCombat.AddChild(inCombatDecision);
        actions.AddChild(isInCombat);

        tree.AddChild(actions);
    }

}
