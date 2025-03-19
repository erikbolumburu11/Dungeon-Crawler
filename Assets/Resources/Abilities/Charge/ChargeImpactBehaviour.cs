using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeImpactBehaviour : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float hitCooldownDuration;
    [SerializeField] float stunTime;
    bool collisionsActive = true;

    void Start(){
        StartCoroutine(DestroySelfAfterTime(2.0f));
        StartCoroutine(DisableCollisions(2.0f));
    }

    void OnTriggerEnter2D(Collider2D other){
        if(!collisionsActive) return;
        if(other.tag == "Character"){
            Team myTeam = GetComponent<TeamComponent>().team;
            Team otherTeam = other.GetComponent<TeamComponent>().team;
            if(myTeam == otherTeam) return;

            if(other.TryGetComponent(out Health enemyHealth)){
                enemyHealth.Damage(damage, hitCooldownDuration, DamageType.PHYSICAL, null);
            }

            if(other.TryGetComponent(out CharacterLocomotion characterLocomotion)){
                characterLocomotion.SetStatusEffectOnTimer(StatusEffect.STUNNED, stunTime);
            }
        }
    }

    IEnumerator DestroySelfAfterTime(float time){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    IEnumerator DisableCollisions(float time){
        yield return new WaitForSeconds(time);
        collisionsActive = false;
    }
}
