using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAbilityAction : AbilityAction
{
    public override string Name => "Trap";

    public override void Invoke(AbilityCastData castData)
    {
        CharacterAbilities abilities = castData.caster.GetComponent<CharacterAbilities>();

        if(abilities.placedTraps.Count >= abilities.maxTraps){ 
            GameObject trapToRemove = abilities.placedTraps[0];

            abilities.placedTraps.Remove(trapToRemove);
            GameObject.Destroy(trapToRemove);
        }

        GameObject trap = GameObject.Instantiate(castData.abilityPrefab, castData.caster.transform.position, Quaternion.identity);
        abilities.placedTraps.Add(trap);

        trap.GetComponent<TrapBehaviour>().owner = castData.caster;
    }
}
