using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public static void SpawnPlayerCharacter(ClassInfo classInfo){
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player);
        }

        GameObject playerSpawnpoint = GameObject.FindWithTag("Player Spawnpoint");

        if(playerSpawnpoint == null){
            Debug.Log("No Object Tagged With Player Spawnpoint Found!");
            return;
        }

        Vector2 spawnPosition = playerSpawnpoint.transform.position;

        Instantiate(classInfo.prefab, spawnPosition, Quaternion.identity);
    }
}
