using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct CharacterLevel {
    public float experienceToNextLevel;
    public AbilityInfo abilityUnlocked;

    public int strengthIncrease;
    public int defenseIncrease;
    public int intelligenceIncrease;
    public int agilityIncrease;
}

public class CharacterExperience : MonoBehaviour
{
    [SerializeField] Slider experienceBarSlider;

    public float currentExperience;
    public int currentLevel;
    public int displayLevel => currentLevel + 1;

    public List<CharacterLevel> characterLevels;

    void Awake()
    {
        if(characterLevels[currentLevel].abilityUnlocked != null){
            if(TryGetComponent(out CharacterAbilities abilities)){
                abilities.equippedAbilites.Add(characterLevels[currentLevel].abilityUnlocked);
            }
        }
    }

    public void AddExperience(float experience)
    {
        currentExperience += experience;

        CheckLevelUp();

        UpdateExperienceBar();
    }

    private void UpdateExperienceBar()
    {
        experienceBarSlider.value = currentExperience / characterLevels[currentLevel].experienceToNextLevel;
    }

    private void CheckLevelUp()
    {
        if (currentExperience >= characterLevels[currentLevel].experienceToNextLevel)
            LevelUp();
    }

    public void LevelUp(){
        currentExperience = characterLevels[currentLevel].experienceToNextLevel - currentExperience;
        currentLevel++;

        if(characterLevels[currentLevel].abilityUnlocked != null){
            if(TryGetComponent(out CharacterAbilities abilities)){
                abilities.equippedAbilites.Add(characterLevels[currentLevel].abilityUnlocked);
            }
        }

        if(TryGetComponent(out CharacterStatistics cs)){
            cs.baseStatistics.strength += characterLevels[currentLevel].strengthIncrease;
            cs.baseStatistics.defense += characterLevels[currentLevel].defenseIncrease;
            cs.baseStatistics.intelligence += characterLevels[currentLevel].intelligenceIncrease;
            cs.baseStatistics.agility += characterLevels[currentLevel].agilityIncrease;
        }

        CheckLevelUp();
    }
}