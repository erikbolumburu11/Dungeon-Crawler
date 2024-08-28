using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
    NEUTRAL,
    FRIENDLY,
    ENEMY
}
public class TeamComponent : MonoBehaviour
{
    public Team team;
}
