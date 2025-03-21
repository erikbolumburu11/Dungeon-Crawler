using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Statistics {
    public int strength;
    public int defense;
    public int intelligence;
    public int agility;
}

public class CharacterStatistics : MonoBehaviour
{
    [SerializeField] public Statistics baseStatistics;

    public Statistics GetStatistics(){
        return new Statistics(){
            strength = baseStatistics.strength,
            defense = baseStatistics.defense,
            intelligence = baseStatistics.intelligence,
            agility = baseStatistics.agility,
        };
    }
}