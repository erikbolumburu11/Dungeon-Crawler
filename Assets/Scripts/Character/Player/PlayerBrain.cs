using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{

    [SerializeField] InputActionReference moveInput;
    [SerializeField] InputActionReference attackInput;

    void OnEnable(){
        attackInput.action.started += Attack;
    }

    void OnDisable(){
        attackInput.action.started -= Attack;
    }

    void Update()
    {
        Movement();
    }

    void Movement(){
        if(TryGetComponent(out CharacterLocomotion characterLocomotion)){
            characterLocomotion.SetMoveDirection(moveInput.action.ReadValue<Vector2>().normalized);
        }
    }

    void Attack(InputAction.CallbackContext callbackContext){
        if(TryGetComponent(out CharacterAttack characterAttack)){
            characterAttack.Attack();
        }
    }
}