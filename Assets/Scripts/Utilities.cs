using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
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

public static class ListExtensions {
    static System.Random rng;

    public static IList<T> Shuffle<T>(this IList<T> list) {
        if (rng == null) rng = new System.Random();
        int count = list.Count;
        while (count > 1) {
            --count;
            int index = rng.Next(count + 1);
            (list[index], list[count]) = (list[count], list[index]);
        }

        return list;
    }
}