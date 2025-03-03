using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Agent Info", menuName = "Agent Info")]
public class AgentInfo : ScriptableObject
{
    [Range(0,20)] public float detectionRange;
    public float visibilityCooldown;

    [Header("Enemy Distance Evaluator")]
    public AnimationCurve preferredDistanceFromEnemy;

    [Header("Agent Distance Evaluator")]
    public float maxDistanceFromSamplePoint;
}
