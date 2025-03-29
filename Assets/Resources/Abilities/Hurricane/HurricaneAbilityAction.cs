using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurricaneAbilityAction : AbilityAction
{
    public override string Name => "Hurricane";

    public override void Invoke(AbilityCastData castData)
    {
        GameObject swords = GameObject.Instantiate(castData.abilityPrefab, castData.caster.transform);

        GameObject.Destroy(swords, 5);
    }
}
