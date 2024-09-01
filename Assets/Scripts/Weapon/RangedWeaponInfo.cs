using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Ranged Weapon Info", menuName = "Weapon/Ranged Weapon Info")]
public class RangedWeaponInfo : EquippableWeaponInfo
{
    bool IsRanged => true;
    public string projectileKey;
}