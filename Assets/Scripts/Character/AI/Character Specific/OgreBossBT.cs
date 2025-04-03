using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using Unity.Mathematics;
using UnityEngine;

public class OgreBossBT : Agent
{
    public bool battleStarted; 

    public float attackLastUsedTime;
    public float attackCooldown;

    public GameObject chargeImpactParticle;

    void Start() {
        attackLastUsedTime = Time.time;

        tree = new BehaviourTree("Human Enemy");

        PrioritySelector actions = new("Agent Logic");
        actions.AddChild(new Leaf("Patrol", new PatrolStrategy(this), 0));

        // Right now, it will always go to MoveToEnemyLastSeenPos as it is a sequence
        // There needs to be a selector somewhere that says: If visible -> chase, else -> move to last seen enemy pos
        Sequence isInCombat = new Sequence("InCombat?", 10);

        isInCombat.AddChild(new Leaf(
            "InCombatCondition",
            new Condition(IsInCombat)
        ));

        PrioritySelector inCombatDecision = new PrioritySelector("Move To Enemy or Attack");

        RandomSelector randomAttackSelector = new RandomSelector("Randomly Select Attack", 5);
        randomAttackSelector.AddChild(new Leaf("Charge Attack", new OgreChargeAttack(this)));

        Sequence canAttackCondition = new Sequence("Can Attack", 5);

        canAttackCondition.AddChild(new Leaf("Can Attack", new Condition(CanAttack)));
        canAttackCondition.AddChild(randomAttackSelector);

        //=====MoveToEnemy=====///
        inCombatDecision.AddChild(new Leaf("MoveToEnemy", new MoveToAttackStrategy(this), 0));

        //=====AttackEnemy=====///

        // Attacks //

        inCombatDecision.AddChild(canAttackCondition);

        isInCombat.AddChild(inCombatDecision);
        actions.AddChild(isInCombat);

        tree.AddChild(actions);
    }

    bool CanAttack(){
        return Time.time - attackLastUsedTime > attackCooldown;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        LevelManager.instance.OpenExit();
    }
}


public class OgreChargeAttack : IStrategy {
    OgreBossBT agent;
    Rigidbody2D rb;

    LayerMask hitLayers;

    Vector2 chargeDir;
    bool chargeDirSet;

    float chargeStartTime;

    public OgreChargeAttack(OgreBossBT agent){
        this.agent = agent;
        rb = agent.gameObject.GetComponent<Rigidbody2D>();

        agent.ResetDestination();

        hitLayers = LayerMask.GetMask("Level") | LayerMask.GetMask("Character");
    }

    public Node.Status Process() {
        agent.ResetDestination();
        if(agent.target == null){
            agent.attackLastUsedTime = Time.time;
            return Node.Status.Success;
        } 

        if(!chargeDirSet){
            agent.GetComponent<CharacterLocomotion>().SetStatusEffect(StatusEffect.CHARGING, true);

            List<GameObject> enemies = TeamComponent.GetOppositeTeamObjectList(agent.GetComponent<TeamComponent>().team);
            List<GameObject> inRangeEnemies = enemies.Where(x => Vector2.Distance(x.transform.position, agent.transform.position) < 20f).ToList();
            int randomIndex = UnityEngine.Random.Range(0, inRangeEnemies.Count - 1);
            agent.target = inRangeEnemies[randomIndex];

            chargeDir = new Vector2(
                agent.target.transform.position.x - agent.transform.position.x,
                agent.target.transform.position.y - agent.transform.position.y
            ).normalized;

            chargeStartTime = Time.time;

            chargeDirSet = true;
        }

        rb.velocity = chargeDir * 10;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(
            agent.movementCollider.bounds.center,
            1.5f,
            hitLayers
        );

        if(Time.time - chargeStartTime < 0.2f) return Node.Status.Running;

        foreach (Collider2D col in hitColliders)
        {
            if(col.gameObject == agent.gameObject) continue;

            if(col.CompareTag("Character"))
            {
                if(col.TryGetComponent(out Health health))
                    health.Damage(70, 0.5f, DamageType.PHYSICAL, null);

                if(col.TryGetComponent(out CharacterLocomotion characterLocomotion))
                    characterLocomotion.SetStatusEffectOnTimer(StatusEffect.STUNNED, 1.0f);

                agent.GetComponent<CharacterLocomotion>().SetStatusEffect(StatusEffect.CHARGING, false);
                chargeDirSet = false;
                agent.attackLastUsedTime = Time.time;
                if(agent.chargeImpactParticle != null){
                    GameObject.Destroy(
                        GameObject.Instantiate(
                            agent.chargeImpactParticle,
                            agent.transform.position,
                            Quaternion.identity
                        )
                    , 1f);
                }
                return Node.Status.Success;
            }

            if(col.CompareTag("Level"))
            {
                agent.GetComponent<CharacterLocomotion>().SetStatusEffect(StatusEffect.CHARGING, false);
                agent.GetComponent<CharacterLocomotion>().SetStatusEffectOnTimer(StatusEffect.STUNNED, 3f);
                chargeDirSet = false;
                agent.attackLastUsedTime = Time.time;
                if(agent.chargeImpactParticle != null){
                    GameObject.Destroy(
                        GameObject.Instantiate(
                            agent.chargeImpactParticle,
                            agent.transform.position,
                            Quaternion.identity
                        )
                    , 1f);
                }
                return Node.Status.Success;
            }

        }

        if(Time.time - chargeStartTime > 4f){
            agent.attackLastUsedTime = Time.time;
            return Node.Status.Success;
        }

        return Node.Status.Running;
    }
}