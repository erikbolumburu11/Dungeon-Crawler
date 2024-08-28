using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public WeaponInfo weaponInfo;
    void OpenHitbox(){
        BoxCollider2D weaponHitbox = GetComponentInChildren<BoxCollider2D>();
        if(weaponHitbox == null) return;
        GetComponentInChildren<BoxCollider2D>().enabled = true;
    }

    void CloseHitbox(){
        BoxCollider2D weaponHitbox = GetComponentInChildren<BoxCollider2D>();
        if(weaponHitbox == null) return;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    public GameObject GetOwner(){
        return GetComponentInParent<CharacterAttack>().gameObject;
    }
}
