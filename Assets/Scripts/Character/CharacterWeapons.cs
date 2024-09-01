using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapons : MonoBehaviour
{
    [SerializeField] Animator weaponAnimator;
    [SerializeField] EquippableWeaponInfo equippedWeapon;
    GameObject instantiatedWeapon;
    [SerializeField] Transform weaponHandlePoint;
    [SerializeField] LookAtMouse weaponMouseFollow;

    void Awake(){
        if(equippedWeapon == null) return;
        EquipWeapon(equippedWeapon);
    }

    void Update(){
        SetWeaponMouseFollow();
    }

    public void SetWeaponMouseFollow(){
        if(TryGetComponent(out CharacterAttack characterAttack)){
            if(characterAttack.IsAttacking() && equippedWeapon.attackingLocksWeaponDir)
                weaponMouseFollow.enabled = false;
            else
                weaponMouseFollow.enabled = true;
        }
    }

    void EquipWeapon(EquippableWeaponInfo weaponInfo){
        equippedWeapon = weaponInfo;
        instantiatedWeapon = Instantiate(equippedWeapon.prefab, weaponHandlePoint);
        weaponAnimator.runtimeAnimatorController = equippedWeapon.animator;
        weaponMouseFollow.rotationOffset = equippedWeapon.weaponLookDirOffset;
        instantiatedWeapon.AddComponent<TeamComponent>().team = GetComponent<TeamComponent>().team;

        foreach(WeaponBehaviour weaponBehaviour in instantiatedWeapon.GetComponentsInChildren<WeaponBehaviour>()){
            weaponBehaviour.weaponInfo = weaponInfo;
        }
    }

    public EquippableWeaponInfo GetEquippedWeaponInfo(){
        return equippedWeapon;
    }

    public GameObject GetInstantiatedWeapon(){
        return instantiatedWeapon;
    }
}