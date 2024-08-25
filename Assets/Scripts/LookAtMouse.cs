using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    void Update(){
        Vector3 screenMousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        Vector2 dir = new Vector3(worldMousePos.x - transform.position.x, worldMousePos.y - transform.position.y);

        transform.up = dir;
    }
}
