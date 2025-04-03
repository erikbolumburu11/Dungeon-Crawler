using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClassSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ClassSelectionMenu menu;
    [SerializeField] ClassInfo classInfo;

    void Awake()
    {
        menu = GetComponentInParent<ClassSelectionMenu>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        menu.UpdateClassPreview(classInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menu.UpdateClassPreview(menu.selectedClass);
    }
}
