using System;
using System.Collections;
using System.Collections.Generic;
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
    public PatrolStrategy(Agent agent){
        this.agent = agent;
    }
    public Node.Status Process() {
        if(Input.GetMouseButton(1)){
            agent.SetDestination(PathfindingGrid.GetTileAtWorldPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
        }
        return Node.Status.Running;
    }
}