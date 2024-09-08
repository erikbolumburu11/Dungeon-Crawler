using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingColliderBehaviour : MonoBehaviour
{
    public GameObject chargeSmashCollider;

    void Start(){
        StartCoroutine(DestroySelfAfterTime(0.5f));
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Level" || other.tag == "Character"){
            Instantiate(chargeSmashCollider, transform.position, Quaternion.identity);

            Destroy(gameObject);

            GameObject player = FindObjectOfType<PlayerBrain>().gameObject;
            CharacterLocomotion characterLocomotion = player.GetComponent<CharacterLocomotion>();
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            characterLocomotion.SetStatusEffect(StatusEffect.CHARGING, false);
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator DestroySelfAfterTime(float time){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
