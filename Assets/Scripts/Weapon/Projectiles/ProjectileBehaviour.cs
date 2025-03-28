using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : WeaponBehaviour
{
    internal Rigidbody2D rigidBody;
    internal ProjectileInfo projectileInfo;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        // rigidBody.velocity = transform.forward * projectileInfo.speed * Time.deltaTime;
        transform.Translate(0, projectileInfo.speed * Time.deltaTime, 0);
    }

    // Must be called from wherever creates this. Very bad.
    public void SetProjectileInfo(ProjectileInfo projectileInfo, Team team, GameObject owner){
        this.projectileInfo = projectileInfo;
        this.owner = owner;
        weaponInfo = projectileInfo;
        GetComponent<TeamComponent>().team = team;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
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
                Destroy(gameObject);
                break;
        }
    }

    internal void InstantiateOnHitEffect(){
        if(projectileInfo.onHitEffectPrefab == null) return;
        Instantiate(projectileInfo.onHitEffectPrefab, transform.position, Quaternion.identity);
    }
}
