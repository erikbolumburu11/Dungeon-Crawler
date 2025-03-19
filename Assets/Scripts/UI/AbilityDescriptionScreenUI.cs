using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityDescriptionScreenUI : MonoBehaviour
{
    [SerializeField] GameObject abilityDescriptionItemPrefab;

    CharacterExperience characterExperience;

    void Awake()
    {
        characterExperience = GetComponentInParent<CharacterExperience>();

        int levelIndex = 1;
        foreach (CharacterLevel level in characterExperience.characterLevels)
        {
            if(level.abilityUnlocked == null){
                levelIndex++;
                continue;
            } 

            AbilityInfo ability = level.abilityUnlocked;
            AbilityDescriptionItem adi = Instantiate(abilityDescriptionItemPrefab, transform).GetComponent<AbilityDescriptionItem>();
            
            adi.SetAbilityDescription(levelIndex, ability.image, ability.abilityName, ability.abilityDescription);

            levelIndex++;
        }
    }
}