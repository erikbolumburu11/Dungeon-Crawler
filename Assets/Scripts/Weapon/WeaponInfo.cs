using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponInfo : ScriptableObject
{
    public string weaponName;
    public GameObject prefab;
    public RuntimeAnimatorController animator;

    [Tooltip("How long until the enemy can be hit again after being hit by this weapon")]
    public float hitCooldown;

    public bool isRanged;

    [HideIf("isRanged")]
    public int damage;
    [HideIf("isRanged")]
    public float knockbackForce;

    [ShowIf("isRanged")]
    public string projectileKey;

    public float weaponLookDirOffset;
    public bool attackingLocksWeaponDir;
    public bool hasSwingAnimation;
    public bool hasSpriteAnimation;
}
