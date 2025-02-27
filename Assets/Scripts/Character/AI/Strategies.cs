using System;
using System.Collections.Generic;
using Sirenix.Utilities;
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

public class MoveToAttackStrategy : IStrategy {
    Agent agent;
    public MoveToAttackStrategy(Agent agent){
        this.agent = agent;
    }

    public Node.Status Process() {
        GameObject target = agent.target;
        if(target == null || target.activeSelf == false) return Node.Status.Failure;

        List<SamplePoint> samplePoints = RingGenerator.GenerateSamplePoints(
            agent.target.transform.position,
            agent.spatialQueryField.ringCount,
            agent.spatialQueryField.pointsPerRing,
            agent.spatialQueryField.distanceBetweenRings
        );

        samplePoints = RingGenerator.DiscardUnwalkablePoints(samplePoints);
        samplePoints = agent.spatialQueryField.EvaluateField(samplePoints, new List<Evaluator>{
            new EnemyDistanceEvaluator(agent.agentInfo.enemyDistanceMagnitude),
            new AgentDistanceEvaluator(agent.agentInfo.agentDistanceMagnitude),
            new VisibiltyEvaluator(agent.agentInfo.visibilityMagnitude)
        });

        agent.SetDestination(
            PathfindingGrid.GetTileAtWorldPosition(
                agent.spatialQueryField.GetBestSamplePoint(samplePoints).pos
            )
        );
        if(Vector2.Distance(agent.transform.position, target.transform.position) > 1) return Node.Status.Running;
        else return Node.Status.Success;
    }
}

public class MoveToEnemyLastSeenPos : IStrategy {
    Agent agent;
    public MoveToEnemyLastSeenPos(Agent agent){
        this.agent = agent;
    }

    public Node.Status Process() {
        if(!agent.IsInCombat()) return Node.Status.Failure;
        if(!agent.GetVisibleEnemies().IsNullOrEmpty()) return Node.Status.Success;

        agent.SetDestination(PathfindingGrid.GetTileAtWorldPosition(agent.posEnemyLastVisible));
        if(Vector2.Distance(agent.transform.position, agent.posEnemyLastVisible) > 1) return Node.Status.Running;
        else return Node.Status.Success;
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