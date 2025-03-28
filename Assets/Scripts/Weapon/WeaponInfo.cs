using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum DamageType {
    PHYSICAL,
    MAGIC,
    AGILITY
}

public class WeaponInfo : ItemInfo
{
    public GameObject prefab;
    public RuntimeAnimatorController animator;

    public AudioClip attackSound;

    public DamageType damageType = DamageType.PHYSICAL;

    bool IsRanged => false;

    [HideIf("IsRanged")]
    [Tooltip("How long until the enemy can be hit again after being hit by this weapon")]
    public float hitCooldown;

    [HideIf("IsRanged")]
    public int damage;

    [HideIf("IsRanged")]
    public float knockbackForce;

}
