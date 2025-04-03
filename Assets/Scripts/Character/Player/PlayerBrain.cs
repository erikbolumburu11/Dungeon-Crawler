using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{

    [SerializeField] InputActionReference moveInput;
    [SerializeField] InputActionReference attackInput;


    [SerializeField] InputActionReference castAbility1Input;
    [SerializeField] InputActionReference castAbility2Input;
    [SerializeField] InputActionReference castAbility3Input;


    void Start()
    {
        TeamComponent.AddToTeamObjectList(gameObject);
    }

    void OnDestroy(){
        TeamComponent.RemoveFromTeamObjectList(gameObject);
    }

    void OnEnable(){
        castAbility1Input.action.started += CastAbility1;
        castAbility2Input.action.started += CastAbility2;
        castAbility3Input.action.started += CastAbility3;
    }

    void OnDisable(){
        castAbility1Input.action.started -= CastAbility1;
        castAbility2Input.action.started -= CastAbility2;
        castAbility3Input.action.started -= CastAbility3;
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

    void CastAbility1(InputAction.CallbackContext callbackContext){
        if(TryGetComponent(out CharacterAbilities abilities)) 
            abilities.CastAbility(0);
    }

    void CastAbility2(InputAction.CallbackContext callbackContext){
        if(TryGetComponent(out CharacterAbilities abilities)) 
            abilities.CastAbility(1);
    }

    void CastAbility3(InputAction.CallbackContext callbackContext){
        if(TryGetComponent(out CharacterAbilities abilities)) 
            abilities.CastAbility(2);
    }

}