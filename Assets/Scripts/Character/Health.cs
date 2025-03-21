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
    CharacterStatistics characterStatistics;

    void Awake()
    {
        characterStatistics = GetComponent<CharacterStatistics>();
    }

    void Start(){
        health = maxHealth; 
    }

    void Update(){
        if(health <= 0) Destroy(gameObject);
    }

    public void Damage(WeaponInfo hitByWeapon, CharacterStatistics hitByCharStats){
        if(!canBeHit) return;

        health -= CalculateDamage(hitByWeapon.damage, hitByWeapon.damageType, hitByCharStats);

        StartCoroutine(StartHitCooldown(hitByWeapon.hitCooldown));

        onHitEvent.Invoke();

        ValueBarUI healthBar = GetComponentInChildren<ValueBarUI>();
        if(healthBar != null && healthBar.CompareTag("Healthbar")) healthBar.UpdateDisplay(health, maxHealth);
    }

    public void Damage(int damage, float hitCooldown, DamageType damageType, CharacterStatistics hitByCharStats){
        if(!canBeHit) return;

        health -= CalculateDamage(damage, damageType, hitByCharStats);

        StartCoroutine(StartHitCooldown(hitCooldown));

        onHitEvent.Invoke();

        ValueBarUI healthBar = GetComponentInChildren<ValueBarUI>();
        if(healthBar != null && healthBar.CompareTag("Healthbar")) healthBar.UpdateDisplay(health, maxHealth);
    }

    int CalculateDamage(int baseDamage, DamageType damageType, CharacterStatistics hitByCharstats){
        int damage = baseDamage;

        if(hitByCharstats != null){
            if(damageType == DamageType.PHYSICAL){
                damage += hitByCharstats.GetStatistics().strength;
            }
            else if(damageType == DamageType.AGILITY){
                damage += hitByCharstats.GetStatistics().agility;
            }
            else if(damageType == DamageType.MAGIC){
                damage += hitByCharstats.GetStatistics().intelligence;
            }
        }

        if(characterStatistics != null){
            damage -= characterStatistics.GetStatistics().defense;
        }

        return damage;
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
                CharacterStatistics hitByCharStats = other.GetComponentInParent<CharacterStatistics>();
                Damage(weaponBehaviour.weaponInfo, hitByCharStats);

                if(TryGetComponent(out Knockback knockback))
                    knockback.Invoke(weaponBehaviour.weaponInfo.knockbackForce, other.gameObject);
            }
        }

    }
}
