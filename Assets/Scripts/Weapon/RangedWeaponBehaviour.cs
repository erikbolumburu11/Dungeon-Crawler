using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponBehaviour : WeaponBehaviour
{
    public void Fire(){
        ProjectileInfo projectileInfo = ProjectileFactory.GetProjectileInfo(weaponInfo.projectileKey);
        if(projectileInfo == null) return;
        
        Vector3 mouseDir = GetDirectionToMouse();

        GameObject projectile = Instantiate(
            projectileInfo.prefab,
            transform.position,
            Quaternion.LookRotation(Vector3.forward, mouseDir)
        );

        projectile.GetComponent<ProjectileBehaviour>()
            .SetProjectileInfo(projectileInfo, GetComponent<TeamComponent>().team);
    }

    Vector2 GetDirectionToMouse(){
        Vector3 screenMousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        Vector2 dir = new Vector3(worldMousePos.x - transform.position.x, worldMousePos.y - transform.position.y);

        return dir;
    }
}
