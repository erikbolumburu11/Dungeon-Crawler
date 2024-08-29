using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : WeaponBehaviour
{
    Rigidbody2D rigidBody;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        // rigidBody.velocity = transform.forward * projectileInfo.speed * Time.deltaTime;
        transform.Translate(0, 1, 0);
    }

    // Must be called from wherever creates this. Very bad.
    public void SetProjectileInfo(ProjectileInfo projectileInfo, Team team){
        this.projectileInfo = projectileInfo;
        GetComponent<TeamComponent>().team = team;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag){
            case "Level":
                if(projectileInfo.destroyedOnLevelCollision){
                    Destroy(gameObject);
                }
                break;
            case "Character":
                Team projectileTeam = GetComponent<TeamComponent>().team;
                Team otherTeam = other.GetComponent<TeamComponent>().team;
                if(projectileTeam == otherTeam) break;

                Destroy(gameObject);
                break;
        }
    }

}
