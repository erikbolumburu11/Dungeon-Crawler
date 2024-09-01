using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponBehaviour : WeaponBehaviour
{
    public void Fire(){
        RangedWeaponInfo rangedWeaponInfo;
        if(weaponInfo is RangedWeaponInfo info)
        {
            rangedWeaponInfo = info;
        }
        else{
            return;
        }

        ProjectileInfo projectileInfo = ProjectileFactory.GetProjectileInfo(rangedWeaponInfo.projectileKey);
        if(projectileInfo == null) return;
        
        Vector3 mouseDir = Utilities.GetDirectionToMouse(transform.position);

        GameObject projectile = Instantiate(
            projectileInfo.prefab,
            transform.position,
            Quaternion.LookRotation(Vector3.forward, mouseDir)
        );

        projectile.GetComponent<ProjectileBehaviour>()
            .SetProjectileInfo(projectileInfo, GetComponent<TeamComponent>().team);
    }

}
