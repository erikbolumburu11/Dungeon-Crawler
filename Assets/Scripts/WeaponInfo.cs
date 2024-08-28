using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Weapon Info", menuName = "Weapon Info")]
public class WeaponInfo : ScriptableObject
{
    public string weaponName;
    public GameObject prefab;
    public RuntimeAnimatorController animator;

    public int damage;
    public float knockbackForce;

    public float weaponLookDirOffset;
    public bool attackingLocksWeaponDir;
    public bool hasSwingAnimation;
    public bool hasSpriteAnimation;
}
