using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public void Invoke(float force, GameObject hitByObject){

        WeaponBehaviour hitByObjectBehaviour = hitByObject.GetComponent<WeaponBehaviour>();
        if(hitByObjectBehaviour == null) return;

        Vector2 hitFrom;

        if(hitByObjectBehaviour is MeleeWeaponBehaviour){
            hitFrom = hitByObject.GetComponentInParent<CharacterAttack>().transform.position;
        }

        else if(hitByObjectBehaviour is ProjectileBehaviour){
            hitFrom = hitByObject.transform.position;
        }

        else return;

        Vector2 knockbackDir = new Vector2(
            transform.position.x - hitFrom.x,
            transform.position.y - hitFrom.y
        ).normalized;

        GetComponent<Rigidbody2D>().AddForce(knockbackDir * force, ForceMode2D.Impulse);

        GetComponent<CharacterLocomotion>().knockedBack = true;

        StartCoroutine(EndKnockback(0.1f));
    }

    IEnumerator EndKnockback(float delay){
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<CharacterLocomotion>().knockedBack = false;;
    }
}
