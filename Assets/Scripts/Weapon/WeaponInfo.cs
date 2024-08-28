using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon Info", menuName = "Weapon Info")]
public class WeaponInfo : ScriptableObject
{
    public string weaponName;
    public GameObject prefab;
    public RuntimeAnimatorController animator;

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
