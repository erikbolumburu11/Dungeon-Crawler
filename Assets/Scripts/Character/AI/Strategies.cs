using System;
using UnityEngine;
using UnityEngine.AI;

public interface IStrategy {
    Node.Status Process();
    void Reset(){}
}

public class ActionStrategy : IStrategy {
    readonly Action action;

    public ActionStrategy(Action action){
        this.action = action;
    }

    public Node.Status Process() {
        action();
        return Node.Status.Success;
    }
}

public class Condition : IStrategy {
    readonly Func<bool> predicate;

    public Condition(Func<bool> predicate) {
        this.predicate = predicate;
    }

    public Node.Status Process(){
        return predicate() ? Node.Status.Success : Node.Status.Failure;
    }
}

public class AttackTargetStrategy : IStrategy {
    GameObject entity;
    GameObject target;
    bool finished;
    public AttackTargetStrategy(GameObject entity, GameObject target){
        this.entity = entity;
        this.target = target;
    }

    public Node.Status Process(){
        return Node.Status.Running;
    }
}

public class MoveToPreferredCombatDistanceStrategy : IStrategy {
    NavMeshAgent agent;
    GameObject target;
    float moveSpeed;
    float preferredCombatDistance;
    bool isPathCalculated;

    public MoveToPreferredCombatDistanceStrategy(NavMeshAgent agent, GameObject target, float preferredCombatDistance, float moveSpeed){
        this.agent = agent;
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.preferredCombatDistance = preferredCombatDistance;
    }
    
    public Node.Status Process() {
        return Node.Status.Running;
    }
}

public class ChaseStrategy : IStrategy {
    NavMeshAgent agent;
    GameObject target;
    float chaseSpeed;
    public ChaseStrategy(NavMeshAgent agent, GameObject target, float chaseSpeed){
        this.agent = agent;
        this.target = target;
        this.chaseSpeed = chaseSpeed;
    }

    public Node.Status Process() {
        return Node.Status.Success;
    }
}

public class PatrolStrategy : IStrategy {
    Agent agent;
    bool wantsNewDestination = true;

    public PatrolStrategy(Agent agent){
        this.agent = agent;
    }

    public Node.Status Process() {
        if(!wantsNewDestination) return Node.Status.Running;

        int agentXPos = Mathf.FloorToInt(agent.transform.position.x);
        int agentYPos = Mathf.FloorToInt(agent.transform.position.y);

        agent.currentPath = null;
        while(agent.currentPath == null){
            int randomX = UnityEngine.Random.Range(-5, 5);
            int randomY = UnityEngine.Random.Range(-5, 5);

            GridTile destination = PathfindingGrid.instance.tiles[agentXPos + randomX, agentYPos + randomY];

            agent.SetDestination(destination);
            wantsNewDestination = false;
        }

        ActionOnTimer.GetTimer(agent.gameObject, "New Patrol Destination").SetTimer
        (
            3.0f,
            () => { wantsNewDestination = true; }
        );

        return Node.Status.Running;
    }
}