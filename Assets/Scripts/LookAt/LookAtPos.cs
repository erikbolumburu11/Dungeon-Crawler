using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPos : LookAtComponent
{
    public float rotationOffset;

    [SerializeField] bool lerp;
    [SerializeField] float lerpSpeed;

    void Update(){
        if(targetPos == null) return;

        Vector2 dir = new Vector3(targetPos.x - transform.position.x, targetPos.y - transform.position.y);

        Vector2 rotatedDir = Quaternion.AngleAxis(rotationOffset, Vector3.forward) * dir;

        transform.up = Vector2.Lerp(transform.up, rotatedDir, lerpSpeed);
    }
}
