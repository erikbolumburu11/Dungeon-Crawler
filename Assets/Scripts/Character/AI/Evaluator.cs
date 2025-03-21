using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[Serializable]
public struct EvaluatorSetting {
    [ShowInInspector] public string className;
    [ShowInInspector] public float weight;

    public EvaluatorSetting(string className, float weight){
        this.className = className;
        this.weight = weight;
    }
}

public abstract class Evaluator
{
    public float weight;

    public abstract float EvaluatePoint(SamplePoint point, Agent agent);

    public Evaluator(float weight){
        this.weight = weight;
    }
}

public class EnemyDistanceEvaluator : Evaluator
{
    public EnemyDistanceEvaluator(float weight) : base(weight)
    {
    }

    public override float EvaluatePoint(SamplePoint point, Agent agent)
    {
        float score = agent.agentInfo.preferredDistanceFromEnemy.Evaluate(Vector2.Distance(agent.target.transform.position, point.pos));
        return score;
    }
}

public class AgentDistanceEvaluator : Evaluator
{
    public AgentDistanceEvaluator(float weight) : base(weight)
    {
    }

    public override float EvaluatePoint(SamplePoint point, Agent agent)
    {
        float score = (agent.agentInfo.maxDistanceFromSamplePoint - Vector2.Distance(agent.transform.position, point.pos)) / agent.agentInfo.maxDistanceFromSamplePoint;
        if(score < 0) score = 0;
        else if(score > 1) score = 1;
        return score;
    }
}

public class VisibilityEvaluator : Evaluator
{
    public VisibilityEvaluator(float weight) : base(weight)
    {
    }

    public override float EvaluatePoint(SamplePoint point, Agent agent)
    {
            Vector2 dir = (agent.target.transform.position - new Vector3(point.pos.x, point.pos.y)).normalized;
            if(!Physics2D.Raycast(point.pos, dir, Vector2.Distance(point.pos, agent.target.transform.position), LayerMask.GetMask("Level"))){
                return 1;
            }
            return 0;
    }
}

public class AllyDistanceEvaluator : Evaluator
{
    public AllyDistanceEvaluator(float weight) : base(weight)
    {
    }

    public override float EvaluatePoint(SamplePoint point, Agent agent)
    {
        List<GameObject> allyObjects = TeamComponent.GetMatchingTeamObjectList(agent.teamComponent.team);
        if(allyObjects.IsNullOrEmpty()) return 0;

        float score = 0;
        foreach (GameObject ally in allyObjects)
        {
            score += (agent.agentInfo.maxDistanceFromAlly - Vector2.Distance(agent.transform.position, ally.transform.position)) / agent.agentInfo.maxDistanceFromAlly;
        }

        score /= allyObjects.Count;

        if(score < 0) score = 0;
        else if(score > 1) score = 1;
        
        return score;
    }
}