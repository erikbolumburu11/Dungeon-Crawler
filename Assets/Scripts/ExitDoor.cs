using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    [SerializeField] GameObject escapedUIObject;
    [SerializeField] GameObject canvasObject;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.TryGetComponent(out PlayerBrain _)) return;
        Instantiate(escapedUIObject, canvasObject.transform);
        PersistanceManager.instance.GotoMainMenuAfterTime(4f);
    }
}