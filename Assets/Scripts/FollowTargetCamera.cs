using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetCamera : MonoBehaviour
{
    Transform target;
    [SerializeField] float followSpeed;

    void FixedUpdate(){
        SetTarget();
        if(target != null) FollowTarget();
    }

    void SetTarget(){
        PlayerBrain playerBrain = FindObjectOfType<PlayerBrain>();
        if(playerBrain == null) return;
        target = playerBrain.transform;
    }

    void FollowTarget(){
        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
