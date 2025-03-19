using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDescriptionItem : MonoBehaviour
{
    [SerializeField] TMP_Text unlockedAtLevelText;
    [SerializeField] Image image;
    [SerializeField] TMP_Text abilityName;
    [SerializeField] TMP_Text abilityDescription;

    public void SetAbilityDescription(int levelUnlocked, Sprite image, string abilityName, string abilityDescription){
        unlockedAtLevelText.text = $"Unlocked at level: {levelUnlocked}";
        this.image.sprite = image;
        this.abilityName.text = abilityName;
        this.abilityDescription.text = abilityDescription;
    }
}