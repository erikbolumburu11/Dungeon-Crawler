using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BouncingFireballBehaviour : ProjectileBehaviour
{
    [SerializeField] int bounceCount;
    [SerializeField] float bounceRange;

    int remainingBounces;

    void Start()
    {
        remainingBounces = bounceCount;
    }

    public override void OnTriggerEnter2D(Collider2D other){
        switch(other.tag){
            case "Level":
                if(projectileInfo.destroyedOnLevelCollision){
                    InstantiateOnHitEffect();
                    Destroy(gameObject);
                }
                break;
            case "Character":
                Team projectileTeam = GetComponent<TeamComponent>().team;
                Team otherTeam = other.GetComponent<TeamComponent>().team;
                if(projectileTeam == otherTeam) break;

                InstantiateOnHitEffect();
                Bounce();
                break;
        }
    }

    void Bounce(){
        if(remainingBounces <= 0) Destroy(gameObject);
        remainingBounces--;

        List<GameObject> enemyObjects = TeamComponent.GetOppositeTeamObjectList(owner.GetComponent<TeamComponent>().team);

        enemyObjects = enemyObjects.Where(
            x => Vector2.Distance(transform.position, x.transform.position) <= bounceRange
        ).ToList();

        List<GameObject> visibleEnemies = new();
        foreach (GameObject enemy in enemyObjects)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            if(!Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position, enemy.transform.position), LayerMask.GetMask("Level"))){
                visibleEnemies.Add(enemy);
            }
        }
        visibleEnemies = visibleEnemies.OrderBy(
            x => Vector2.Distance(transform.position, x.transform.position)
        ).ToList();

        if(visibleEnemies.Count <= 1) Destroy(gameObject);

        visibleEnemies.RemoveAt(0);

        GameObject target = visibleEnemies[0];

        Vector2 dir = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);

        Vector2 rotatedDir = Quaternion.AngleAxis(0, Vector3.forward) * dir;

        transform.up = rotatedDir;

        //transform.up = targetDir;
    }
}
