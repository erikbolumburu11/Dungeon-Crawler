using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanEnemy : Agent
{
    void Start() {
        tree = new BehaviourTree("Human Enemy");

        Sequence actions = new("Agent Logic");

        actions.AddChild(new Leaf("Patrol", new PatrolStrategy(this), 0));
        tree.AddChild(actions);
    }

}
