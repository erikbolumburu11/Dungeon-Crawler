using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Ability Info", menuName = "Ability Info")]
public class AbilityInfo : ScriptableObject
{
    public string abilityName;
    public string abilityActionKey;
    public GameObject prefab;
    public Image image;
}
