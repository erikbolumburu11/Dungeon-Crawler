using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using System;

public class EvaluatorSettingDrawer : OdinValueDrawer<EvaluatorSetting>
{
    private string className;
    private string weight;

    protected override void DrawPropertyLayout(GUIContent label)
    {
        className = ValueEntry.SmartValue.className.ToString();
        className = SirenixEditorFields.TextField(label, className);

        weight = ValueEntry.SmartValue.weight.ToString();
        weight = SirenixEditorFields.TextField(label, weight);

        if(float.TryParse(weight, out float weightValue)){
            ValueEntry.SmartValue = new(className, weightValue);
        }
        
    }
}
