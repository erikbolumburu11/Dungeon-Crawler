using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueBarUI : MonoBehaviour
{
    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateDisplay(float currentValue, float maxValue){
        float fillPercent = currentValue / maxValue;
        slider.value = fillPercent;
    }
}
