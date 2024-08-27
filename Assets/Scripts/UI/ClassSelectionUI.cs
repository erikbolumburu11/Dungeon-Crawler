using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelectionUI : MonoBehaviour
{
    [SerializeField] GameObject classButtonPrefab;
    [SerializeField] Transform classButtonGrid;

    List<GameObject> instantiatedButtons;

    void Awake(){
        instantiatedButtons = new();
    }

    void OnEnable(){
        DestroyButtons();

        Object[] classes = Resources.LoadAll("", typeof(ClassInfo));

        foreach(Object c in classes){
            ClassInfo classInfo = (ClassInfo)c;
            GameObject button = Instantiate(classButtonPrefab, classButtonGrid);

            button.GetComponentInChildren<TMP_Text>().text = classInfo.className;

            button.GetComponent<Button>().onClick.AddListener(() => {
                CharacterSpawner.SpawnPlayerCharacter(classInfo);
                gameObject.SetActive(false);
            });

            instantiatedButtons.Add(button);
        }
    }

    void OnDisable() {
        DestroyButtons();
    }

    void DestroyButtons(){
        foreach(GameObject button in instantiatedButtons){
            Destroy(button);
        }
    }
}
