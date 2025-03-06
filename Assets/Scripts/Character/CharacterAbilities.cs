using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    public List<AbilityInfo> equippedAbilites;
    public Dictionary<int, bool> readyToCast;

    void Awake()
    {
       readyToCast = new();
    }

    public void CastAbility(int index){
        if(!readyToCast.ContainsKey(index)) readyToCast.Add(index, true);
        if(!readyToCast[index]) return; 

        readyToCast[index] = false;
        ActionOnTimer.GetTimer(gameObject, $"Ability{index}").SetTimer(
            equippedAbilites[index].abilityCooldown,
            () => {
                if(!readyToCast.TryAdd(index, true)){
                    readyToCast[index] = true;
                }
            }
        );

        AbilityInfo ability = equippedAbilites[index];

        AbilityCastData castData = new AbilityCastData(){
            caster = gameObject,
            abilityInfo = ability,
            abilityPrefab = ability.prefab
        };

        AbilityFactory.GetAbility(ability.abilityActionKey).Invoke(castData);
    }
}