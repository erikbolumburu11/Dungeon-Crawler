using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    Animator weaponAnimator;
    [SerializeField] GameObject weaponLookOrbit;
    [SerializeField] LookAtMouse weaponMouseFollow;

    bool isAttacking;

    void Start(){
        weaponAnimator = weapon.GetComponent<Animator>();
    }

    void Update(){
        isAttacking = weaponAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        SetWeaponMouseFollow();
    }

    public void SetWeaponMouseFollow(){
        if(isAttacking)
            weaponMouseFollow.enabled = false;
        else
            weaponMouseFollow.enabled = true;
    }

    public void Attack(){
        if(isAttacking) return;

        StartCoroutine(LerpWeaponToAttackRotation(0.1f, 1));
        weaponAnimator.SetTrigger("Attack");
    }

    IEnumerator LerpWeaponToAttackRotation(float time, float speed){
        float elapsedTime = 0;

        Vector3 screenMousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        // Change to Vec2
        Vector2 attackDir = new Vector3(
            worldMousePos.x - weaponLookOrbit.transform.position.x,
            worldMousePos.y - weaponLookOrbit.transform.position.y
        );

        Vector2 rotatedDir = Quaternion.AngleAxis(weaponMouseFollow.GetRotationOffset(), Vector3.forward) * attackDir;

        while(elapsedTime < time){
            weaponLookOrbit.transform.up = Vector2.Lerp(transform.up, rotatedDir, speed);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        weaponLookOrbit.transform.up = rotatedDir;
        yield return null;
    }


}
