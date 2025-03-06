using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueBarUI : MonoBehaviour
{
    Slider slider;
    [SerializeField] Image fillArea;

    void Start()
    {
        slider = GetComponent<Slider>();
        TeamComponent tc = GetComponentInParent<TeamComponent>();
        PlayerBrain pb = GetComponentInParent<PlayerBrain>();
        if(tc != null && pb == null){
            if(tc.team == Team.NEUTRAL || tc.team == Team.ENEMY){
                fillArea.color = Color.red;
            }
            else{
                fillArea.color = Color.green;
            }
        }
    }

    public void UpdateDisplay(float currentValue, float maxValue){
        float fillPercent = currentValue / maxValue;
        slider.value = fillPercent;
    }
}
