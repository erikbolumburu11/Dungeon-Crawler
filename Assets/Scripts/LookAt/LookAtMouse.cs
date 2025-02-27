using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : LookAtComponent
{
    public float rotationOffset;

    [SerializeField] bool lerp;
    [SerializeField] float lerpSpeed;

    void Update(){
        Vector3 screenMousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        Vector2 dir = new Vector3(worldMousePos.x - transform.position.x, worldMousePos.y - transform.position.y);

        Vector2 rotatedDir = Quaternion.AngleAxis(rotationOffset, Vector3.forward) * dir;

        transform.up = Vector2.Lerp(transform.up, rotatedDir, lerpSpeed);
    }
}
