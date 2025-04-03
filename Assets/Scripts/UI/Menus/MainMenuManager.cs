using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject currentMenu;
    public Stack<GameObject> cameFrom;

    void Awake()
    {
        cameFrom = new();
    }

    public void OpenMenu(GameObject menu){
        currentMenu.SetActive(false);
        menu.SetActive(true);

        cameFrom.Push(currentMenu);

        currentMenu = menu;
    }

    public void Back(){
        if(cameFrom.Count == 0) return;

        GameObject previousMenu = cameFrom.Pop();

        currentMenu.SetActive(false);
        previousMenu.SetActive(true);

        currentMenu = previousMenu;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
