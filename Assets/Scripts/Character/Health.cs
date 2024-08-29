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

    public void Damage(GameObject hitByObject){
        if(!canBeHit) return;

        WeaponBehaviour hitByObjectBehaviour = hitByObject.GetComponent<WeaponBehaviour>();

        if(hitByObjectBehaviour == null) return;

        int damage = 0;
        float knockbackForce = 0;

        if(hitByObjectBehaviour is MeleeWeaponBehaviour){
            WeaponInfo weaponInfo = hitByObjectBehaviour.weaponInfo;

            damage = weaponInfo.damage;
            knockbackForce = weaponInfo.knockbackForce;
        }

        else if(hitByObjectBehaviour is ProjectileBehaviour){
            ProjectileInfo projectileInfo = hitByObjectBehaviour.projectileInfo;

            damage = projectileInfo.damage;
            knockbackForce = projectileInfo.knockbackForce;
        }

        health -= damage;

        StartCoroutine(StartHitCooldown(hitByObjectBehaviour.weaponInfo.hitCooldown));

        if(TryGetComponent(out Knockback knockback)){
            knockback.Invoke(knockbackForce, hitByObject);
        }

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

            if(myTeam != otherTeam || otherTeam == Team.NEUTRAL)
                Damage(other.gameObject);
        }

    }
}
