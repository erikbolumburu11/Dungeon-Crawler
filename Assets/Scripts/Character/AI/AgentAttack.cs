using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{
    [SerializeField] Animator weaponAnimator;
    [SerializeField] float attackRange;
    Agent agent;

    void Awake()
    {
        agent = GetComponent<Agent>();
    }

    void Update()
    {
        GameObject target = agent.target;
        if(target == null) return;

        Vector2 dir = (target.transform.position - transform.position).normalized;
        float dist = Vector2.Distance(transform.position, target.transform.position);
        if(dist - 1 <= attackRange){
            if(!Physics2D.Raycast(transform.position, dir, dist, LayerMask.GetMask("Level"))){
                Attack();
            }
        }
    }

    void Attack(){
        GetComponent<CharacterAttack>().Attack();
    }
}
