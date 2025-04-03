using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbilityAction : AbilityAction
{
    public override string Name => "Fireball";

    public override void Invoke(AbilityCastData castData)
    {
        ProjectileInfo projectileInfo = ProjectileFactory.GetProjectileInfo("FireballProjectile");
        if(projectileInfo == null) return;
        
        Vector2 playerPos = castData.caster.transform.position;
        Vector2 castPos = new(playerPos.x, playerPos.y + 1f);

        Vector3 mouseDir = Utilities.GetDirectionToMouse(castPos);

        GameObject projectile = GameObject.Instantiate(
            projectileInfo.prefab,
            castPos,
            Quaternion.LookRotation(Vector3.forward, mouseDir)
        );

        projectile.GetComponent<ProjectileBehaviour>()
            .SetProjectileInfo(projectileInfo, castData.caster.GetComponent<TeamComponent>().team, castData.caster);
    }
}