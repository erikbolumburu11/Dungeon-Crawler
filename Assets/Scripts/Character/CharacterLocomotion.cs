using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator bodyAnimator;
    [SerializeField] SpriteRenderer bodySprite;
    Rigidbody2D rigidBody;

    
    [Header("Character Data")]
    public float moveSpeed;
    const float moveSpeedMultiplier = 100; // Set move speed to 10 instead of 1000, just looks nicer.

    public bool knockedBack;

    Vector2 moveDir;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetLookDirection();

        bodyAnimator.SetBool("Moving", moveDir.magnitude > 0);
    }

    void FixedUpdate() {
        if(knockedBack) return;

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
}
