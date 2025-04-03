using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class TalkBehaviour : MonoBehaviour
{
    List<DialogueController> dialogueControllers;

    [SerializeField] InputActionReference talkInput;

    void Awake()
    {
        dialogueControllers = new();
    }

    void OnEnable()
    {
        talkInput.action.started += Talk;
    }

    void OnDisable()
    {
        talkInput.action.started -= Talk;
    }

    public void Talk(InputAction.CallbackContext callbackContext){
        if(dialogueControllers.IsNullOrEmpty()) return;

        DialogueController dc = dialogueControllers.OrderBy(
            x => Vector2.Distance(transform.position, x.transform.position)
        ).ToList()[0];

        if(dc.readyToBeSpokenTo){
            if(!dc.conversationStarted) dc.StartDialogue();
            else dc.ContinueDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out DialogueController dc)){
            dialogueControllers.Add(dc);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.TryGetComponent(out DialogueController dc)){
            dialogueControllers.Remove(dc);
        }
    }
}
