using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Class Info", menuName = "Class Info")]
public class ClassInfo : ScriptableObject
{
    public string className;
    public GameObject prefab;
    public Sprite previewSprite;
}
