using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Projectile Info", menuName = "Projectile Info")]
public class ProjectileInfo : ScriptableObject
{
    public string projectileKey;
    public GameObject prefab;
    public float speed;
    public int damage;
    public float knockbackForce;
    public bool destroyedOnLevelCollision;
}

