using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class Agent : MonoBehaviour
{
    protected BehaviourTree tree;
    CharacterLocomotion locomotion;
    public TeamComponent teamComponent;
    public AgentInfo agentInfo;
    public SpatialQueryField spatialQueryField;
    LookAtPos lookAtPos;
    TargetSelection targetSelection;

    [SerializeField] Collider2D movementCollider;

    public Stack<GridTile> currentPath;
    protected GridTile destinationTile;

    public GameObject target;

    float timeEnemyLastVisible;
    public Vector3 posEnemyLastVisible;
    Dictionary<GameObject, float> enemyLastSeenTimeMap;

    public float experienceGivenOnDeath;
    public const float XPRANGE = 50;

    [Header("Debug")]
    [SerializeField] string currentStateName;
    [SerializeField] bool drawPath;
    [SerializeField] bool drawAgentInfo;
    [SerializeField] bool drawDetectRange;

    public void Awake() {
        locomotion = GetComponent<CharacterLocomotion>();
        teamComponent = GetComponent<TeamComponent>();
        spatialQueryField = GetComponent<SpatialQueryField>();
        targetSelection = GetComponent<TargetSelection>();
        lookAtPos = GetComponentInChildren<LookAtPos>();

        enemyLastSeenTimeMap = new();

        TeamComponent.AddToTeamObjectList(gameObject);
    }

    public void OnDestroy(){
        TeamComponent.RemoveFromTeamObjectList(gameObject);
        GiveExperience();
    }

    void Update() {
        tree.Process();
        currentStateName = tree.GetRunningNode().name;

        if(locomotion != null) MoveToDestination();        

        if(targetSelection != null) target = targetSelection.SelectTarget(this);
        if(target != null) posEnemyLastVisible = target.transform.position;

        if(!GetVisibleEnemies().IsNullOrEmpty()) timeEnemyLastVisible = Time.time;

        AimWeapon();
    }

    void AimWeapon(){
        if(lookAtPos == null) return;
        if(target != null){
            lookAtPos.SetTargetPos(target.transform.position);
        }
        else{
            if(currentPath != null && currentPath.Count > 0){
                lookAtPos.SetTargetPos(currentPath.Peek().worldPosition);
            }
        }
    }

    public void SetDestination(GridTile destination){
        destinationTile = destination;
        currentPath = GetComponent<Pathfinder>().FindPath(
            PathfindingGrid.GetTileAtWorldPosition(transform.position),
            destinationTile,
            movementCollider
        );
    }


    public void ResetDestination()
    {
        destinationTile = null;
    }

    void MoveToDestination(){
        if(destinationTile == null || currentPath == null || currentPath.Count == 0){
            locomotion.SetMoveDirection(Vector2.zero);
            return;
        } 

        GridTile nextTileInPath = currentPath.Peek();
        if(Vector3.Distance(transform.position, nextTileInPath.worldPosition) < 0.05f){
            currentPath.Pop();
        }
        Vector2 moveDir = (nextTileInPath.worldPosition - transform.position).normalized;
        locomotion.SetMoveDirection(moveDir);
    }

    public List<GameObject> GetVisibleEnemies(){
        List<GameObject> enemyObjects = TeamComponent.GetOppositeTeamObjectList(teamComponent.team);

        enemyObjects = enemyObjects.Where(
            x => Vector2.Distance(transform.position, x.transform.position) <= agentInfo.detectionRange
        ).ToList();

        List<GameObject> visibleEnemies = new();
        foreach (GameObject enemy in enemyObjects)
        {
            Vector2 dir = (enemy.transform.position - transform.position).normalized;
            if(!Physics2D.Raycast(transform.position, dir, Vector2.Distance(transform.position, enemy.transform.position), LayerMask.GetMask("Level"))){
                if(enemyLastSeenTimeMap.ContainsKey(enemy))
                    enemyLastSeenTimeMap[enemy] = Time.time;
                else
                    enemyLastSeenTimeMap.Add(enemy, Time.time);
            }
        }
        List<GameObject> enemiesToRemove = new();
        foreach(KeyValuePair<GameObject, float> enemy in enemyLastSeenTimeMap){
            if(enemy.Key == null) enemiesToRemove.Add(enemy.Key);
            if(Time.time - enemy.Value < agentInfo.visibilityCooldown){
                visibleEnemies.Add(enemy.Key);
            }
        }
        foreach (GameObject enemy in enemiesToRemove)
        {
            enemyLastSeenTimeMap.Remove(enemy);
            visibleEnemies.Remove(enemy);
        }
        visibleEnemies = visibleEnemies.OrderBy(
            x => Vector2.Distance(transform.position, x.transform.position)
        ).ToList();
        return visibleEnemies;
    }

    public bool IsInCombat(){
        if(
            !GetVisibleEnemies().IsNullOrEmpty() || 
            (Time.time > 5 && Time.time - timeEnemyLastVisible < 5) ||
            targetSelection.recentHitEntries.Count > 0
        ) return true;

        return false;
    }

    private void GiveExperience()
    {
        List<GameObject> enemies = TeamComponent.GetOppositeTeamObjectList(teamComponent.team);

        enemies = enemies.Where(
            x => Vector2.Distance(transform.position, x.transform.position) <= XPRANGE
        ).ToList();

        foreach (GameObject enemy in enemies)
            if(enemy.TryGetComponent(out CharacterExperience characterExperience))
                characterExperience.AddExperience(experienceGivenOnDeath);
    }

    #if UNITY_EDITOR
    void OnDrawGizmos(){
        #region DRAW_PATH
        if(drawPath && currentPath != null) {
            Gizmos.color = Color.blue;

            GridTile[] path = currentPath.ToArray();
            Vector3[] pathPositions = path.Select(path => path.worldPosition).ToArray();

            for (int i = 0; i < path.Length - 1; i++)
            {
                Gizmos.DrawLine(pathPositions[i], pathPositions[i+1]);
            }
        }
        #endregion DRAW_PATH

        #region TARGET_LINE
        if(target != null){
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
        #endregion TARGET_LINE

        #region DRAW_DETECT_RANGE
        if(drawDetectRange){
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, agentInfo.detectionRange);
        }
        #endregion DRAW_DETECT_RANGE

        #region AGENT_INFO
        if(tree != null){
            UnityEditor.Handles.color = Color.white;
            Node runningNode = tree.GetRunningNode();
            if(drawAgentInfo && runningNode != null){
                UnityEditor.Handles.Label(transform.position, runningNode.name);
            }
        }
        #endregion AGENT_INFO
    }
#endif
}
