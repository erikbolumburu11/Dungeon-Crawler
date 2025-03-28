using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScattershotAbilityAction : AbilityAction
{
    public override string Name => "Scattershot";

    public override void Invoke(AbilityCastData castData)
    {
        ProjectileInfo projectileInfo = ProjectileFactory.GetProjectileInfo("Wooden Arrow");
        if(projectileInfo == null) return;
        
        Vector2 playerPos = Utilities.GetCenterOfObject(castData.caster);
        Vector3 mouseDir = Utilities.GetDirectionToMouse(playerPos);

        // Cone
        float coneSpreadAngle = 15f;
        for (int i = -1; i < castData.abilityInfo.projectileCount - 1; i++)
        {
            float shotAngleOffset = coneSpreadAngle * i; 
            
            Vector3 shotDir = Quaternion.AngleAxis(shotAngleOffset, Vector3.forward) * mouseDir;

            GameObject projectile = GameObject.Instantiate(
                projectileInfo.prefab,
                playerPos,
                Quaternion.LookRotation(Vector3.forward, shotDir)
            );

            projectile.GetComponent<ProjectileBehaviour>()
                .SetProjectileInfo(projectileInfo, castData.caster.GetComponent<TeamComponent>().team, castData.caster);
        }

    }
}
