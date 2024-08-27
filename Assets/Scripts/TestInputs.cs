using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputs : MonoBehaviour
{
    [SerializeField] GameObject classSelectionMenu;
    
    void Update(){
        // Toggle Class Selection Menu
        if(Input.GetKeyDown(KeyCode.C)){
            classSelectionMenu.SetActive(!classSelectionMenu.activeSelf);
        }
    }
}