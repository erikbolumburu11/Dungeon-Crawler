using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    bool characterScreenOpen;
    [SerializeField] InputActionReference toggleCharacterDisplay;
    [SerializeField] GameObject characterDisplay;

    bool abilityScreenOpen;
    [SerializeField] InputActionReference toggleAbilityDescriptionDisplay;
    [SerializeField] GameObject abilityDescriptionDisplay;

    bool inventoryScreenOpen;
    [SerializeField] InputActionReference toggleInventoryDisplay;
    [SerializeField] GameObject inventoryDisplay;

    void OnEnable()
    {
        toggleCharacterDisplay.action.started += ToggleCharacterDisplay;
        toggleAbilityDescriptionDisplay.action.started += ToggleAbilityDisplay;
        toggleInventoryDisplay.action.started += ToggleInventoryDisplay;
    }

    void OnDisable()
    {
        toggleCharacterDisplay.action.started -= ToggleCharacterDisplay;
        toggleAbilityDescriptionDisplay.action.started -= ToggleAbilityDisplay;
        toggleInventoryDisplay.action.started -= ToggleInventoryDisplay;
    }

    void ToggleCharacterDisplay(InputAction.CallbackContext callbackContext){
        characterScreenOpen = !characterScreenOpen;
        characterDisplay.SetActive(characterScreenOpen);
    }

    void ToggleAbilityDisplay(InputAction.CallbackContext callbackContext){
        abilityScreenOpen = !abilityScreenOpen;
        abilityDescriptionDisplay.SetActive(abilityScreenOpen);
    }

    void ToggleInventoryDisplay(InputAction.CallbackContext callbackContext){
        inventoryScreenOpen = !inventoryScreenOpen;
        inventoryDisplay.SetActive(inventoryScreenOpen);
    }
}
