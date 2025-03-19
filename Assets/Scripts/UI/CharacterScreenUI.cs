using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterScreenUI : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text strengthText;
    [SerializeField] TMP_Text defenseText;
    [SerializeField] TMP_Text intelligenceText;
    [SerializeField] TMP_Text agilityText;

    CharacterStatistics stats;
    CharacterExperience experience;

    void Awake()
    {
        stats = GetComponentInParent<CharacterStatistics>();
        experience = GetComponentInParent<CharacterExperience>();
    }

    void Update()
    {
        levelText.text = $"Level: {experience.displayLevel}";
        strengthText.text = $"Strength: {stats.strength}";
        defenseText.text = $"Defense: {stats.defense}";
        intelligenceText.text = $"Intelligence: {stats.intelligence}";
        agilityText.text = $"Agility: {stats.agility}";
    }
}
