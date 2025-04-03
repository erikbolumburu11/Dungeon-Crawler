using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAbilityAction : AbilityAction
{
    public override string Name => "Charge";

    public override void Invoke(AbilityCastData castData)
    {
        ProjectileInfo projectileInfo = ProjectileFactory.GetProjectileInfo("FireballProjectile");
        if(projectileInfo == null) return;
        
        Vector2 playerPos = castData.caster.transform.position;
        Vector3 mouseDir = Utilities.GetDirectionToMouse(playerPos);

        Rigidbody2D rb = castData.caster.GetComponent<Rigidbody2D>();

        castData.caster.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // NOTE: Instead of a timer, it should allow movement again when the player hits a wall or enemy.
        castData.caster.GetComponent<CharacterLocomotion>().SetStatusEffectOnTimer(
            StatusEffect.CHARGING,
            0.2f
        );

        rb.AddForce(mouseDir * 30, ForceMode2D.Impulse);

        castData.caster.GetComponent<CharacterLocomotion>().SetStatusEffectOnTimer(
            StatusEffect.INVINCIBLE,
            0.4f
        );

        GameObject chargingPlayerCollider = Resources.Load("Abilities/Charge/ChargingCollider") as GameObject;

        GameObject.Instantiate(chargingPlayerCollider, castData.caster.transform);
    }
}