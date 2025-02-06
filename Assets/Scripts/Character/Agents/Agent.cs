using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    protected BehaviourTree tree;
    CharacterLocomotion locomotion;

    public Stack<GridTile> currentPath;
    protected GridTile destinationTile;

    [Header("Debug")]
    [SerializeField] string currentStateName;
    [SerializeField] bool drawPath;
    [SerializeField] bool drawAgentInfo;

    public void Awake() {
        locomotion = GetComponent<CharacterLocomotion>();
    }

    void Update() {
        tree.Process();
        currentStateName = tree.GetRunningNode().name;

        if(locomotion != null) MoveToDestination();        
    }

    public void SetDestination(GridTile destination){
        destinationTile = destination;
        currentPath = GetComponent<Pathfinder>().FindPath(PathfindingGrid.GetTileAtWorldPosition(transform.position), destinationTile);
    }

    void MoveToDestination(){
        if(destinationTile == null || currentPath == null || currentPath.Count == 0){
            locomotion.SetMoveDirection(Vector2.zero);
            return;
        } 

        GridTile nextTileInPath = currentPath.Peek();
        if(Vector3.Distance(transform.position, nextTileInPath.worldPosition) < 0.3f){
            currentPath.Pop();
        }
        Vector2 moveDir = (nextTileInPath.worldPosition - transform.position).normalized;
        locomotion.SetMoveDirection(moveDir);
    }

    public bool IsPlayerVisible() {
/*         if(Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) < detectRange){
            return true;
        }
        return false; */
        throw new NotImplementedException();
    }
    
    protected bool IsPlayerInCombatRange(){
/*         if(Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) - 0.1f <= preferredCombatDistance){
            return true;
        }
        return false; */
        throw new NotImplementedException();
    }

    protected bool CanAttack(){
/*         if(GetComponent<CombatController>().isAttacking){
            return false;
        } 
        return true; */
        throw new NotImplementedException();
    }

    #if UNITY_EDITOR
    void OnDrawGizmos(){
        // Draw Path
        if(drawPath && currentPath != null) {
            Gizmos.color = Color.blue;

            GridTile[] path = currentPath.ToArray();
            Vector3[] pathPositions = path.Select(path => path.worldPosition).ToArray();

            for (int i = 0; i < path.Length - 1; i++)
            {
                Gizmos.DrawLine(pathPositions[i], pathPositions[i+1]);
            }
        }

        // Draw Agent Info
        if(tree != null){
            Node runningNode = tree.GetRunningNode();
            if(drawAgentInfo && runningNode != null){
                UnityEditor.Handles.Label(transform.position, runningNode.name);
            }
        }
    }
    #endif
}
