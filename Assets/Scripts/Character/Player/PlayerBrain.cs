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
        castAbilityInput.action.started += CastAbility;
    }

    void OnDisable(){
        castAbilityInput.action.started  -= CastAbility;
    }

    void Update()
    {
        Movement();
        Attack();
    }

    void Movement(){
        if(TryGetComponent(out CharacterLocomotion characterLocomotion)){
            characterLocomotion.SetMoveDirection(moveInput.action.ReadValue<Vector2>().normalized);
        }
    }

    void Attack(){
        if(attackInput.action.IsPressed()){
            if(TryGetComponent(out CharacterAttack characterAttack)){
                characterAttack.Attack();
            }
        }
    }

    void CastAbility(InputAction.CallbackContext callbackContext){
        if(TryGetComponent(out CharacterAbilities abilities)) 
            abilities.CastAbility(0);
    }
}