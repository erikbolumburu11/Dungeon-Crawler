using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] GameObject exitDoorObject;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void OpenExit(){
        exitDoorObject.SetActive(true);
    }
}
