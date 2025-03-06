using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHotbarUI : MonoBehaviour
{
    [SerializeField] Image[] abilityImageSlots;
    [SerializeField] Slider[] abilitySliders;
    [SerializeField] Color emptySlotColor;

    CharacterAbilities characterAbilities;

    void Awake()
    {
        characterAbilities = GetComponentInParent<CharacterAbilities>();
    }

    void Update()
    {
        UpdateImages();
        UpdateSliders();
    }

    void UpdateSliders()
    {
        List<AbilityInfo> equippedAbiltiies = characterAbilities.equippedAbilites;
        for (int i = 0; i < abilityImageSlots.Length; i++)
        {
            if(i < equippedAbiltiies.Count){
                if(!characterAbilities.readyToCast.ContainsKey(i) || characterAbilities.readyToCast[i]){
                    abilitySliders[i].value = 0;
                    continue;
                }
                ActionOnTimer cooldownTimer = ActionOnTimer.GetTimer(characterAbilities.gameObject, $"Ability{i}");
                abilitySliders[i].value = cooldownTimer.remainingTime / cooldownTimer.timerDuration;

            }
        }
    }

    void UpdateImages(){
        List<AbilityInfo> equippedAbiltiies = characterAbilities.equippedAbilites;
        for (int i = 0; i < abilityImageSlots.Length; i++)
        {
            if(i < equippedAbiltiies.Count){
                abilityImageSlots[i].sprite = equippedAbiltiies[i].image;
                abilityImageSlots[i].color = Color.white;
            }
            else{
                abilityImageSlots[i].sprite = null;
                abilityImageSlots[i].color = emptySlotColor;
            }
        }
    }
}
