using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClassSelectionMenu : MonoBehaviour
{
    public ClassInfo selectedClass;

    [SerializeField] Image previewImage;
    [SerializeField] TMP_Text classNameText;
    [SerializeField] TMP_Text statText;

    void Start()
    {
        UpdateClassPreview(selectedClass);
    }

    public void UpdateClassPreview(ClassInfo classInfo){
        Sprite sprite = classInfo.previewSprite;
        previewImage.sprite = sprite;

        Statistics classStats = classInfo.prefab.GetComponent<CharacterStatistics>().baseStatistics;

        string strength = $"Strength: {classStats.strength}\n";
        string defense = $"Defense: {classStats.defense}\n";
        string intelligence = $"Intelligence: {classStats.intelligence}\n";
        string agility = $"Agility: {classStats.agility}";

        statText.text = strength + defense + intelligence + agility;

        classNameText.text = classInfo.className;
    }

    public void SetSelectedClass(ClassInfo classInfo){
        selectedClass = classInfo;
    }

    public void StartGame(){
        PersistanceManager.instance.StartGame(selectedClass);
        SceneManager.LoadScene("SampleScene");
    }
}
