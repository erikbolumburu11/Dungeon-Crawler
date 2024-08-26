using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    void OpenHitbox(){
        GetComponentInChildren<BoxCollider2D>().enabled = true;
    }

    void CloseHitbox(){
        GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    public GameObject GetOwner(){
        return GetComponentInParent<CharacterAttack>().gameObject;
    }
}
