using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    Animator weaponSwingAnimator;
    [SerializeField] GameObject weaponLookOrbit;
    bool attackRequested;

    void Start(){
        weaponSwingAnimator = weapon.GetComponent<Animator>();
    }

    public void Attack(){
        if(IsAttacking()) return;

        // Use Equipped Weapon To Attack
        if(TryGetComponent(out CharacterWeapons characterWeapons)){
            StartCoroutine(LerpWeaponToAttackDirection(0.1f, 1));

            WeaponInfo equippedWeapon = characterWeapons.GetEquippedWeaponInfo();

            // Sprite Animation
            if(equippedWeapon.hasSpriteAnimation){
                if(characterWeapons.GetInstantiatedWeapon() != null){
                    Animator weaponSpriteAnimator = characterWeapons.GetInstantiatedWeapon().GetComponent<Animator>();
                    weaponSpriteAnimator.SetTrigger("Attack");
                }
            }

            // Swing Animation
            if(equippedWeapon.hasSwingAnimation){
                weaponSwingAnimator.SetTrigger("Attack");
            }

        }
        // Use Body To Attack
        else{

        }

    }

    IEnumerator LerpWeaponToAttackDirection(float time, float speed){
        CharacterWeapons characterWeapons = GetComponent<CharacterWeapons>();
        if(characterWeapons == null) yield break;

        float elapsedTime = 0;

        Vector3 screenMousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        // Change to Vec2
        Vector2 attackDir = new Vector3(
            worldMousePos.x - weaponLookOrbit.transform.position.x,
            worldMousePos.y - weaponLookOrbit.transform.position.y
        );

        Vector2 rotatedDir = Quaternion.AngleAxis(characterWeapons.GetEquippedWeaponInfo().weaponLookDirOffset, Vector3.forward) * attackDir;

        while(elapsedTime < time){
            weaponLookOrbit.transform.up = Vector2.Lerp(transform.up, rotatedDir, speed);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        weaponLookOrbit.transform.up = rotatedDir;
        yield return null;
    }

    public bool IsAttacking(){
        if(TryGetComponent(out CharacterWeapons characterWeapons)){
            bool isSpriteAnimating = false;
            if(characterWeapons.GetEquippedWeaponInfo().hasSpriteAnimation){
                isSpriteAnimating = characterWeapons.GetInstantiatedWeapon()
                    .GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Attack");
            }

            bool isSwingAnimating = false;
            if(characterWeapons.GetEquippedWeaponInfo().hasSwingAnimation){
                isSwingAnimating = weaponSwingAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
            }

            return isSpriteAnimating || isSwingAnimating;
        }
        return false;
    }
}
