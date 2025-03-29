using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbilityAction : AbilityAction
{
    public override string Name => "Dash";

    public override void Invoke(AbilityCastData castData)
    {
        Vector2 playerPos = castData.caster.transform.position;
        Vector3 mouseDir = Utilities.GetDirectionToMouse(playerPos);

        Rigidbody2D rb = castData.caster.GetComponent<Rigidbody2D>();

        castData.caster.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        rb.AddForce(mouseDir * 20, ForceMode2D.Impulse);

        castData.caster.GetComponent<CharacterLocomotion>().SetStatusEffectOnTimer(
            StatusEffect.CHARGING,
            0.2f
        );

        castData.caster.GetComponent<CharacterLocomotion>().SetStatusEffectOnTimer(
            StatusEffect.INVINCIBLE,
            0.2f
        );
    }
}
