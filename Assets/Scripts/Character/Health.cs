using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int health;
    bool canBeHit = true;
    [SerializeField] float hitCooldown;
    [SerializeField] UnityEvent onHitEvent;

    void Start(){
        health = maxHealth; 
    }

    void Update(){
        if(health <= 0) Destroy(gameObject);
    }

    public void Damage(GameObject hitByObject){
        if(!canBeHit) return;

        WeaponInfo hitByWeaponInfo = hitByObject.GetComponent<WeaponBehaviour>().weaponInfo;
        if(hitByWeaponInfo == null) return;

        StartHitCooldown();

        health -= hitByWeaponInfo.damage;

        onHitEvent.Invoke();

        if(TryGetComponent(out Knockback knockback)){
            knockback.Invoke(hitByWeaponInfo.knockbackForce, hitByObject);
        }

        ValueBarUI healthBar = GetComponentInChildren<ValueBarUI>();
        if(healthBar != null && healthBar.CompareTag("Healthbar")) healthBar.UpdateDisplay(health, maxHealth);
    }

    IEnumerator StartHitCooldown(){
        canBeHit = false;
        yield return new WaitForSeconds(hitCooldown);
        canBeHit = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out WeaponBehaviour weaponBehaviour)){
            Team myTeam = GetComponent<TeamComponent>().team;
            Team otherTeam = other.GetComponent<TeamComponent>().team;

            if(myTeam != otherTeam || otherTeam == Team.NEUTRAL)
                Damage(other.gameObject);
        }

    }
}
