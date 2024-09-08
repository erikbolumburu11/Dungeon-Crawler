using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect {
    KNOCKED_BACK,
    CHARGING,
    STUNNED
}

public class CharacterLocomotion : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator bodyAnimator;
    [SerializeField] SpriteRenderer bodySprite;
    Rigidbody2D rigidBody;

    [Header("Character Data")]
    public float moveSpeed;

    public Dictionary<StatusEffect, bool> statusEffects;

    Vector2 moveDir;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        statusEffects = new();
    }

    void Update()
    {
        SetLookDirection();

        bodyAnimator.SetBool("Moving", moveDir.magnitude > 0);
    }

    void FixedUpdate() {
        if(!CanMove()) return;

        rigidBody.velocity = moveDir * moveSpeed;
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