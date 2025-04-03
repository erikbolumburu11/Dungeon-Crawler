using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect {
    KNOCKED_BACK,
    CHARGING,
    STUNNED,
    INVINCIBLE
}

public class CharacterLocomotion : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator bodyAnimator;
    [SerializeField] SpriteRenderer bodySprite;
    Rigidbody2D rigidBody;
    CharacterStatistics characterStatistics;

    [Header("Character Data")]
    public float moveSpeed;
    float baseMoveSpeed;

    public int objectAvoidanceRayCount = 3;
    public float objectAvoidanceRayLength = 1;
    public float objectAvoidanceConeAngle = 30;

    public Dictionary<StatusEffect, bool> statusEffects;

    Vector2 moveDir;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        baseMoveSpeed = moveSpeed;
        characterStatistics = GetComponent<CharacterStatistics>();
        
        statusEffects = new();
    }

    void Update()
    {
        SetLookDirection();

        bodyAnimator.SetBool("Moving", moveDir.magnitude > 0.2f);

        UpdateMoveSpeed();

        if(!CanMove()) SetMoveDirection(Vector2.zero);
        if(statusEffects.ContainsKey(StatusEffect.STUNNED) && statusEffects[StatusEffect.STUNNED]){
            bodyAnimator.SetBool("Moving", false);
            rigidBody.velocity = Vector2.zero;
        }

    }

    private void UpdateMoveSpeed()
    {
        if(characterStatistics == null) return;
        moveSpeed = baseMoveSpeed + (characterStatistics.GetStatistics().agility * 0.25f);
    }

    void FixedUpdate() {
        if(!CanMove()) return;
        if(moveDir == Vector2.zero){
            if(statusEffects.ContainsKey(StatusEffect.CHARGING)){
                if(statusEffects[StatusEffect.CHARGING] == false){
                    rigidBody.velocity = Vector2.zero;
                }
            }
            else{
                rigidBody.velocity = Vector2.zero;
            }
            return;
        } 

        // Object Avoidance
        Vector2 longestDir = moveDir;
        float longestLength = 0;
        int middleRay = Mathf.FloorToInt(objectAvoidanceRayCount / 2);
        for (int i = -middleRay; i < objectAvoidanceRayCount - middleRay; i++)
        {
            float shotAngleOffset = objectAvoidanceConeAngle * i; 
            Vector3 rayDir = Quaternion.AngleAxis(shotAngleOffset, Vector3.forward) * moveDir;
            

            float rayLength;
            if(i == 0) rayLength = objectAvoidanceRayLength + 1.5f;
            else rayLength = objectAvoidanceRayLength;

            Debug.DrawRay(transform.position, rayDir.normalized * rayLength, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir * rayLength, rayLength);

            if(hit.collider == null){
                if(rayLength > longestLength){
                    longestLength = rayLength;
                    longestDir = rayDir;
                }
            }
            else{
                if(Vector2.Distance(transform.position, hit.point) > longestLength){
                    longestLength = Vector2.Distance(transform.position, hit.point);
                    longestDir = rayDir;
                }
            }
        }

        rigidBody.velocity = longestDir.normalized * moveSpeed;
    }

    void SetLookDirection(){
        if(moveDir.magnitude <= 0) return;

        if(moveDir.x > 0)       bodySprite.flipX = false;
        else if(moveDir.x < 0)  bodySprite.flipX = true;
    }

    public void SetMoveDirection(Vector2 moveDir){
        this.moveDir = moveDir;
    }

    public void SetStatusEffect(StatusEffect statusEffect, bool isActive){
        if(statusEffects.ContainsKey(statusEffect)) 
            statusEffects[statusEffect] = isActive;

        else statusEffects.Add(statusEffect, isActive);
    }

    public bool GetStatusEffect(StatusEffect statusEffect){
        if(statusEffects.ContainsKey(statusEffect)) 
            return statusEffects[statusEffect];

        else return false;
    }

    public void SetStatusEffectOnTimer(StatusEffect effect, float time){
        StartCoroutine(SetStatusEffectOnTimerCoroutine(effect, time));
    }

    IEnumerator SetStatusEffectOnTimerCoroutine(StatusEffect effect, float time){
        SetStatusEffect(effect, true);
        yield return new WaitForSeconds(time);
        SetStatusEffect(effect, false);
    }

    bool CanMove(){
        return !(
            GetStatusEffect(StatusEffect.KNOCKED_BACK) || 
            GetStatusEffect(StatusEffect.CHARGING) ||
            GetStatusEffect(StatusEffect.STUNNED)
        );
    }

}