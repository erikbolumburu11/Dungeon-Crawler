using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAbilityAction : AbilityAction
{
    public override string Name => "Explosion";

    public override void Invoke(AbilityCastData castData)
    {
        GameObject explosionPrefab = castData.abilityInfo.prefab;

        Vector3 screenMousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
        worldMousePos.z = 0;

        GameObject explosion = GameObject.Instantiate(
            explosionPrefab,
            worldMousePos,
            Quaternion.identity
        );

        explosion.GetComponentInChildren<TeamComponent>().team = castData.caster.GetComponent<TeamComponent>().team;
    }
}