using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponBehaviour : WeaponBehaviour
{
    public void Fire(){
        ProjectileInfo projectileInfo = ProjectileFactory.GetProjectileInfo(weaponInfo.projectileKey);
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
