using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LookAtComponent : MonoBehaviour
{
    protected Vector3 targetPos;

    public void SetTargetPos(Vector3 pos){
        targetPos = pos;
    }
}
