using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Default Ability Info", menuName = "Ability Info")]
public class AbilityInfo : ScriptableObject
{
    public string abilityName;
    public string abilityActionKey;
    public float abilityCooldown;
    public bool hasMaxCastRange;
    [ShowIf("hasMaxCastRange")] public float maxCastRange;
    public int projectileCount;
    public GameObject prefab;
    public Sprite image;
    public string abilityDescription;
}
