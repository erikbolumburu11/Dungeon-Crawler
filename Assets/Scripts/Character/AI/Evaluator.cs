using UnityEngine;

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

public class VisibiltyEvaluator : Evaluator
{
    public VisibiltyEvaluator(float weight) : base(weight)
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