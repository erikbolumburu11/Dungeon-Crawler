using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public void Invoke(float force, GameObject hitByObject){

        WeaponBehaviour hitByObjectBehaviour = hitByObject.GetComponent<WeaponBehaviour>();
        if(hitByObjectBehaviour == null) return;


        Vector2 knockbackDir;

        if(hitByObjectBehaviour is MeleeWeaponBehaviour){
            Vector2 hitFrom = hitByObject.GetComponentInParent<CharacterAttack>().transform.position;
            knockbackDir = new Vector2(
                transform.position.x - hitFrom.x,
                transform.position.y - hitFrom.y
            ).normalized;
        }

        else if(hitByObjectBehaviour is ProjectileBehaviour){
            knockbackDir = hitByObject.transform.up;
        }

        else return;

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
