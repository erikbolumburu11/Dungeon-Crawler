using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int health;
    [SerializeField] UnityEvent onHitEvent;

    void Start(){
        health = maxHealth; 
    }

    void Update(){
        if(health <= 0) Destroy(gameObject);
    }

    public void Damage(int damage, GameObject hitByObject){
        health -= damage;

        onHitEvent.Invoke();

        if(TryGetComponent(out Knockback knockback)){
            knockback.Invoke(10f, hitByObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player Weapon")) Damage(10, other.gameObject);
    }
}
