using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator bodyAnimator;
    [SerializeField] InputActionReference moveInput;
    Rigidbody2D rigidBody;

    
    [Header("Character Data")]
    public float moveSpeed;
    const float moveSpeedMultiplier = 100; // Set move speed to 10 instead of 1000, just looks nicer.

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveDir = moveInput.action.ReadValue<Vector2>().normalized;

        rigidBody.velocity = moveDir * moveSpeed * Time.deltaTime * moveSpeedMultiplier;

        bodyAnimator.SetBool("Moving", rigidBody.velocity.magnitude > 0.2f);
    }
}
