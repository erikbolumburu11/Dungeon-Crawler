using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponInfo : ScriptableObject
{
    public string weaponName;
    public GameObject prefab;
    public RuntimeAnimatorController animator;


    bool IsRanged => false;

    [HideIf("IsRanged")]
    [Tooltip("How long until the enemy can be hit again after being hit by this weapon")]
    public float hitCooldown;

    [HideIf("IsRanged")]
    public int damage;

    [HideIf("IsRanged")]
    public float knockbackForce;

}
