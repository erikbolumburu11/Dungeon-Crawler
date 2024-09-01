using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static Vector2 GetDirectionToMouse(Vector2 originPos){
        Vector3 screenMousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(screenMousePos);

        Vector2 dir = new Vector3(worldMousePos.x - originPos.x, worldMousePos.y - originPos.y).normalized;

        return dir;
    }

    public static Vector2 GetCenterOfObject(GameObject obj){
        if(obj.TryGetComponent(out Collider2D col)){
            return col.bounds.center;
        }
        return obj.transform.position;
    }
}