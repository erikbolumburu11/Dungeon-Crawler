using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulwarkAbilityAction : AbilityAction
{
    public override string Name => "Bulwark";

    public override void Invoke(AbilityCastData castData)
    {
        Health health = castData.caster.GetComponent<Health>();
        health.armour += 100;
    }
}