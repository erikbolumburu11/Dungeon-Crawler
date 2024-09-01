using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    public List<AbilityInfo> equippedAbilites;

    public void CastAbility(int index){
        AbilityInfo ability = equippedAbilites[index];

        AbilityCastData castData = new AbilityCastData(){
            caster = gameObject,
            abilityInfo = ability,
            abilityPrefab = ability.prefab
        };

        AbilityFactory.GetAbility(ability.abilityActionKey).Invoke(castData);
    }
}
