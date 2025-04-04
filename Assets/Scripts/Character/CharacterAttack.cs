using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    Animator weaponSwingAnimator;
    [SerializeField] GameObject weaponLookOrbit;
    CharacterLocomotion characterLocomotion;
    bool attackRequested;

    void Start(){
        weaponSwingAnimator = weapon.GetComponent<Animator>();
        characterLocomotion = GetComponent<CharacterLocomotion>();
    }

    public void Attack(){
        if(IsAttacking()) return;
        if(!CanAttack()) return;

        // Use Equipped Weapon To Attack
        if(TryGetComponent(out CharacterWeapons characterWeapons)){
            StartCoroutine(LerpWeaponToAttackDirection(0.1f, 1));

            EquippableWeaponInfo equippedWeapon = characterWeapons.GetEquippedWeaponInfo();

            if(equippedWeapon.attackSound != null)
                SoundFXManager.instance.PlaySoundFXClip(equippedWeapon.attackSound, transform, 1.0f, 0.2f);

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

        Vector3 targetLookPos = GetComponentInChildren<LookAtComponent>().GetTargetPos();

        // Change to Vec2
        Vector2 attackDir = new Vector3(
            targetLookPos.x - weaponLookOrbit.transform.position.x,
            targetLookPos.y - weaponLookOrbit.transform.position.y
        ).normalized;

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

    bool CanAttack(){
        return !(
            characterLocomotion.GetStatusEffect(StatusEffect.KNOCKED_BACK) || 
            characterLocomotion.GetStatusEffect(StatusEffect.CHARGING) ||
            characterLocomotion.GetStatusEffect(StatusEffect.STUNNED)
        );
    }
}
