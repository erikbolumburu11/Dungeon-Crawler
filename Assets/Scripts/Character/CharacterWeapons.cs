using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapons : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] EquippableWeaponInfo startingWeapon;
    GameObject instantiatedWeapon;
    [SerializeField] Transform weaponHandlePoint;
    [SerializeField] LookAtMouse weaponMouseFollow;

    void Awake(){
        inventory = GetComponent<Inventory>();
        if(startingWeapon == null) return;
        EquipWeapon(startingWeapon);
    }

    void Update(){
        SetWeaponMouseFollow();
    }

    public void SetWeaponMouseFollow(){
        if(weaponMouseFollow == null) return;

        if(TryGetComponent(out CharacterAttack characterAttack)){
            if(characterAttack.IsAttacking() && GetEquippedWeaponInfo().attackingLocksWeaponDir)
                weaponMouseFollow.setRotation = false;
            else
                weaponMouseFollow.setRotation = true;
        }
    }

    public void EquipWeapon(EquippableWeaponInfo weaponInfo){
        if(instantiatedWeapon != null) Destroy(instantiatedWeapon);
        if(GetEquippedWeaponInfo() != null) inventory.AddItem(GetEquippedWeaponInfo());

        inventory.equippedItems[EquipSlot.WEAPON] = weaponInfo;
        instantiatedWeapon = Instantiate(GetEquippedWeaponInfo().prefab, weaponHandlePoint);
        weaponAnimator.runtimeAnimatorController = GetEquippedWeaponInfo().animator;

        if(weaponMouseFollow != null)
            weaponMouseFollow.rotationOffset = GetEquippedWeaponInfo().weaponLookDirOffset;

        instantiatedWeapon.AddComponent<TeamComponent>().team = GetComponent<TeamComponent>().team;

        foreach(WeaponBehaviour weaponBehaviour in instantiatedWeapon.GetComponentsInChildren<WeaponBehaviour>()){
            weaponBehaviour.weaponInfo = weaponInfo;
        }
    }

    public EquippableWeaponInfo GetEquippedWeaponInfo(){
        return (EquippableWeaponInfo)inventory.equippedItems[EquipSlot.WEAPON];
    }

    public GameObject GetInstantiatedWeapon(){
        return instantiatedWeapon;
    }
}