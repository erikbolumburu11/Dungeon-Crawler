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
        if(equippedAbilites.Count <= index) return;

        AbilityInfo ability = equippedAbilites[index];

        if(ability.hasMaxCastRange){
            Vector3 screenMousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
            worldMousePos.z = 0;

            if(Vector2.Distance(transform.position, worldMousePos) > ability.maxCastRange) return;
        }

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

        AbilityCastData castData = new AbilityCastData(){
            caster = gameObject,
            abilityInfo = ability,
            abilityPrefab = ability.prefab
        };

        AbilityFactory.GetAbility(ability.abilityActionKey).Invoke(castData);
    }
}