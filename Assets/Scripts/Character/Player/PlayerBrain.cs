using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{

    [SerializeField] InputActionReference moveInput;
    [SerializeField] InputActionReference attackInput;
    [SerializeField] InputActionReference castAbilityInput;

    void OnEnable(){
        attackInput.action.started += Attack;
        castAbilityInput.action.started += CastAbility;
    }

    void OnDisable(){
        attackInput.action.started -= Attack;
        castAbilityInput.action.started  -= CastAbility;
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

    void CastAbility(InputAction.CallbackContext callbackContext){
        if(TryGetComponent(out CharacterAbilities abilities)) 
            abilities.CastAbility(0);
    }
}