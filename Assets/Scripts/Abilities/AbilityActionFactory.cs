using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class AbilityFactory
{
    static Dictionary<string, Type> abilitiesByName;
    static bool IsInitialized => abilitiesByName != null;

    static void InitializeFactory(){
        if(IsInitialized) return;

        var abilityTypes = Assembly.GetAssembly(typeof(AbilityAction)).GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(AbilityAction)));

        abilitiesByName = new Dictionary<string, Type>();

        foreach (var type in abilityTypes){
            var tempAbility = Activator.CreateInstance(type) as AbilityAction;
            abilitiesByName.Add(tempAbility.Name, type);
        }
    }

    public static AbilityAction GetAbility(string abilityType){
        InitializeFactory();

        if(abilitiesByName.ContainsKey(abilityType)){
            Type type = abilitiesByName[abilityType];
            var ability = Activator.CreateInstance(type) as AbilityAction;
            return ability;
        }

        Debug.Log($"Ability {abilityType} not found!");
        return null;
    }

    public static IEnumerable<string> GetAbilityNames(){
        InitializeFactory();
        return abilitiesByName.Keys;
    }

}
