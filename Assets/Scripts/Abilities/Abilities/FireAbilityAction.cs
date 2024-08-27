using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbilityAction : AbilityAction
{
    public override string Name => "Fire";

    public override void Invoke(AbilityCastData castData)
    {
        Debug.Log($"Casted Fire Ability");

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        GameObject.Instantiate(
            castData.abilityPrefab,
            mouseWorldPos,
            Quaternion.identity
        );
    }
}
