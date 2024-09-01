using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Projectile Info", menuName = "Projectile Info")]
public class ProjectileInfo : WeaponInfo
{
    public string key;
    public float speed;
    public bool destroyedOnLevelCollision;
    public GameObject onHitEffectPrefab;
}