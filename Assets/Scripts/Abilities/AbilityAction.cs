using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCastData {
    public GameObject caster;
    public GameObject abilityPrefab;
}

public abstract class AbilityAction {
    public abstract string Name { get; }

    public abstract void Invoke(AbilityCastData castData);
}
