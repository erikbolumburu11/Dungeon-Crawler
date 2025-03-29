using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    public int damage;
    public float stunTime;

    public GameObject owner;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Character"){
            Team myTeam = GetComponent<TeamComponent>().team;
            Team otherTeam = other.GetComponent<TeamComponent>().team;
            if(myTeam == otherTeam) return;

            if(other.TryGetComponent(out Health enemyHealth)){
                enemyHealth.Damage(damage, 0, DamageType.PHYSICAL, null);
            }

            if(other.TryGetComponent(out CharacterLocomotion characterLocomotion)){
                characterLocomotion.SetStatusEffectOnTimer(StatusEffect.STUNNED, stunTime);
            }

            owner.GetComponent<CharacterAbilities>().placedTraps.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
