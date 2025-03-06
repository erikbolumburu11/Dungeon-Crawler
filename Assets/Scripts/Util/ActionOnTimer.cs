using System;
using UnityEngine;

public class ActionOnTimer : MonoBehaviour
{
    public new string name = "Timer";
    public float remainingTime;
    public float timerDuration;

    private Action timerCallback;

    public void SetTimer(float time, Action timerCallback) {
        this.remainingTime = time;
        this.timerDuration = time;
        this.timerCallback = timerCallback;
    }

    private void Update() {
        if(IsRunning()) {
            remainingTime -= Time.deltaTime;

            if(remainingTime <= 0) timerCallback();
        }
    }

    public bool IsRunning(){
        return remainingTime > 0;
    }

    public static ActionOnTimer GetTimer(GameObject obj, string name){
        ActionOnTimer[] timers = obj.GetComponents<ActionOnTimer>();
        foreach (ActionOnTimer timer in timers)
        {
            if(timer.name == name) return timer;
        }
        ActionOnTimer newTimer = obj.AddComponent<ActionOnTimer>();
        newTimer.name = name;
        return newTimer;
    }
}