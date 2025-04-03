using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

[Serializable]
public struct TextEntry {
    [Multiline(4)]public string text;
    public bool endOfConversation;
    [ShowIf("endOfConversation")] public bool summonsAlly;
    [ShowIf("endOfConversation")] public bool destroysSelf;
    [ShowIf("summonsAlly")] public GameObject allyToSummon;
}

public class DialogueController : MonoBehaviour
{
    
    public bool readyToBeSpokenTo;
    [SerializeField] GameObject readyNotification;

    public bool conversationStarted;

    [SerializeField] GameObject textBox;
    RectTransform textBoxRectTransform;
    [SerializeField] TMP_Text text;
    [SerializeField] Vector2 textboxAnchorPos;

    public TextEntry[] dialogue;
    int currentDialogueIndex = 0;

    void Awake()
    {
        textBoxRectTransform = textBox.GetComponent<RectTransform>();
        if(readyToBeSpokenTo) readyNotification.SetActive(true);
    }

    void Update()
    {
        if(conversationStarted){
            textBoxRectTransform.anchoredPosition = new Vector3(
                textboxAnchorPos.x,
                textboxAnchorPos.y + (textBoxRectTransform.sizeDelta.y / 2f)
            );
        }
    }

    public void StartDialogue(){
        conversationStarted = true;
        readyNotification.SetActive(false);
        textBox.SetActive(true);
        text.text = dialogue[0].text;
    }

    public void ContinueDialogue(){
        currentDialogueIndex++; 

        int i = currentDialogueIndex;

        if(dialogue[i].endOfConversation){
            readyToBeSpokenTo = false;
            textBox.SetActive(false);

            if(dialogue[i].summonsAlly){
                Instantiate(dialogue[i].allyToSummon, transform.position, Quaternion.identity);
                if(dialogue[i].destroysSelf) Destroy(gameObject);
            }
        }

        text.text = dialogue[i].text;
    }
}
