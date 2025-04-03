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
        samplePoints = agent.spatialQueryField.EvaluateField(samplePoints);

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

        if(agent.spawnTile == null) return Node.Status.Running;

        int agentXPos = Mathf.FloorToInt(agent.spawnTile.worldPosition.x);
        int agentYPos = Mathf.FloorToInt(agent.spawnTile.worldPosition.y);

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

public class FollowStrategy : IStrategy {
    Agent agent;
    GridTile destinationTile;
    PlayerBrain playerBrain;

    public FollowStrategy(Agent agent){
        this.agent = agent;
    }

    public Node.Status Process() {
        if(playerBrain == null){
            playerBrain = GameObject.FindObjectOfType<PlayerBrain>();
            if(playerBrain == null) return Node.Status.Failure;
        } 

        GameObject player = playerBrain.gameObject;

        if(destinationTile != null && Vector2.Distance(destinationTile.worldPosition, player.transform.position) < 4){
            agent.SetDestination(destinationTile);
            return Node.Status.Running;
        }

        int playerXPos = Mathf.FloorToInt(player.transform.position.x);
        int playerYPos = Mathf.FloorToInt(player.transform.position.y);

        agent.currentPath = null;
        while(agent.currentPath == null){
            int randomX = UnityEngine.Random.Range(-2, 2);
            int randomY = UnityEngine.Random.Range(-2, 2);

            destinationTile = PathfindingGrid.instance.tiles[playerXPos + randomX, playerYPos + randomY];

            agent.SetDestination(destinationTile);
        }

        return Node.Status.Running;
    }
}