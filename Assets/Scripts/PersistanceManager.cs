using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistanceManager : MonoBehaviour
{
    public static PersistanceManager instance;

    public ClassInfo selectedClassInfo;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame(ClassInfo selectedClassInfo){
        this.selectedClassInfo = selectedClassInfo;
    }

    public void GotoMainMenuAfterTime(float time){
        ActionOnTimer.GetTimer(gameObject, "Return To Main Menu").SetTimer(time, () => {
            GotoMainMenu();
        });
    }

    public void GotoMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
