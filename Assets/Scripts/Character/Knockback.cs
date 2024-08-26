using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public void Invoke(float force, GameObject hitByObject){
        Vector2 hitFrom = hitByObject.GetComponentInParent<CharacterAttack>().transform.position;

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
