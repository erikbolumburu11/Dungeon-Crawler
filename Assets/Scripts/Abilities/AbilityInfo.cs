using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Default Ability Info", menuName = "Ability Info")]
public class AbilityInfo : ScriptableObject
{
    public string abilityName;
    public string abilityActionKey;
    public int projectileCount;
    public GameObject prefab;
    public Image image;
}
