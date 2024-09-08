using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int health;
    bool canBeHit = true;
    [SerializeField] UnityEvent onHitEvent;

    void Start(){
        health = maxHealth; 
    }

    void Update(){
        if(health <= 0) Destroy(gameObject);
    }

    public void Damage(int amount, float hitCooldownDuration){
        if(!canBeHit) return;

        health -= amount;

        StartCoroutine(StartHitCooldown(hitCooldownDuration));

        onHitEvent.Invoke();

        ValueBarUI healthBar = GetComponentInChildren<ValueBarUI>();
        if(healthBar != null && healthBar.CompareTag("Healthbar")) healthBar.UpdateDisplay(health, maxHealth);
    }

    IEnumerator StartHitCooldown(float duration){
        canBeHit = false;
        yield return new WaitForSeconds(duration);
        canBeHit = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out WeaponBehaviour weaponBehaviour)){
            Team myTeam = GetComponent<TeamComponent>().team;
            Team otherTeam = other.GetComponent<TeamComponent>().team;

            if(myTeam != otherTeam || otherTeam == Team.NEUTRAL){
                Damage(weaponBehaviour.weaponInfo.damage, weaponBehaviour.weaponInfo.hitCooldown);

                if(TryGetComponent(out Knockback knockback))
                    knockback.Invoke(weaponBehaviour.weaponInfo.knockbackForce, other.gameObject);
            }
        }

    }
}
