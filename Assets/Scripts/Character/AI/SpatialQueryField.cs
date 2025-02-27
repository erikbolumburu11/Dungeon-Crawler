using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class SpatialQueryField : MonoBehaviour
{
    Agent agent;

    [SerializeField] Gradient pointScoreColorGradient;

    List<SamplePoint> samplePoints;

    public int ringCount;
    public int pointsPerRing;
    public float distanceBetweenRings;

    [Header("Debug")]
    [SerializeField] bool drawSpatialField;

    void Awake()
    {
        agent = GetComponent<Agent>();
    }

    public List<SamplePoint> EvaluateField(List<SamplePoint> samplePoints, List<Evaluator> evaluators){
        foreach (SamplePoint samplePoint in samplePoints)
        {
            float score = 0;
            foreach (Evaluator evaluator in evaluators)
            {
                score += evaluator.EvaluatePoint(samplePoint, agent) / evaluators.Count;
                score *= evaluator.weight;
            }
            samplePoint.score = score;
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
            samplePoints = EvaluateField(samplePoints, new List<Evaluator>{
                new EnemyDistanceEvaluator(agent.agentInfo.enemyDistanceMagnitude),
                new AgentDistanceEvaluator(agent.agentInfo.agentDistanceMagnitude),
                new VisibiltyEvaluator(agent.agentInfo.visibilityMagnitude)
            });
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