using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Tilemap floorTileMap;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(this);
    }

    void Start()
    {
        CharacterSpawner.SpawnPlayerCharacter(PersistanceManager.instance.selectedClassInfo);
    }

    public static GameObject GetPlayerObject(){
        return FindAnyObjectByType<PlayerBrain>().gameObject;
    }

}