using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class SpatialQueryField : MonoBehaviour
{
    Agent agent;

    [SerializeField] Gradient pointScoreColorGradient;

    [TabGroup("Ring Generator Settings")] public int ringCount;
    [TabGroup("Ring Generator Settings")] public int pointsPerRing;
    [TabGroup("Ring Generator Settings")] public float distanceBetweenRings;

    [TabGroup("Evaluators")] public List<EvaluatorSetting> activeEvaluators;
    List<object> evaluators;

    [Button(ButtonSizes.Medium), TabGroup("Evaluators")]
    public void UpdateEvaluators(){
        evaluators = new();
        foreach (EvaluatorSetting evaluatorSetting in activeEvaluators)
        {
            Type type = Type.GetType(evaluatorSetting.className);

            if(type == null){
                Debug.LogError($"Evaluator: {evaluatorSetting.className} does not exist!");
                continue;
            }

            evaluators.Add(Activator.CreateInstance(type, evaluatorSetting.weight));
        }
    }

    [TabGroup("Debug")]
    [SerializeField] bool drawSpatialField;

    void Awake()
    {
        agent = GetComponent<Agent>();
        UpdateEvaluators();
    }

    public List<SamplePoint> EvaluateField(List<SamplePoint> samplePoints){
        foreach (SamplePoint samplePoint in samplePoints)
        {
            float score = 0;
            foreach (object evaluator in evaluators)
            {
                object[] parametersArray = new object[] {samplePoint, agent};
                score += (float)evaluator.GetType().GetMethod("EvaluatePoint").Invoke(evaluator, parametersArray) / evaluators.Count;
                Evaluator e = (Evaluator)evaluator;
                score *= e.weight;
            }
            samplePoint.score = score;
        }

        return NormalizeField(samplePoints);
    }

    public List<SamplePoint> NormalizeField(List<SamplePoint> samplePoints){
        if(samplePoints.IsNullOrEmpty()) return null;
        float bestScore = samplePoints.OrderBy(x => x.score).Reverse().ToList()[0].score;
        foreach (SamplePoint samplePoint in samplePoints)
        {
            samplePoint.score /= bestScore;
        }
        return samplePoints;
    }

    public SamplePoint GetBestSamplePoint(List<SamplePoint> samplePoints){
        if(samplePoints.IsNullOrEmpty()) return null;
        return samplePoints.OrderBy(x => x.score).Reverse().ToList()[0];
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;

        if(drawSpatialField && agent.target != null){
            List<SamplePoint> samplePoints = RingGenerator.GenerateSamplePoints(
                agent.target.transform.position,
                ringCount,
                pointsPerRing,
                distanceBetweenRings
            );

            samplePoints = RingGenerator.DiscardUnwalkablePoints(samplePoints);
            samplePoints = EvaluateField(samplePoints);
            foreach (SamplePoint point in samplePoints)
            {
                Gizmos.color = pointScoreColorGradient.Evaluate(point.score);
                Gizmos.DrawSphere(point.pos, 0.175f);
                UnityEditor.Handles.Label(point.pos, point.score.ToString());
            }
        }
    }
    #endif
}