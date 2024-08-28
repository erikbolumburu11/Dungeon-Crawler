using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileFactory
{
    public static Dictionary<string, ProjectileInfo> projectilesByName;
    static bool IsInitialized => projectilesByName != null;

    static void InitializeFactory(){
        if(IsInitialized) return;

        projectilesByName = new();

        Object[] projectiles = Resources.LoadAll("", typeof(ProjectileInfo));

        foreach(Object proj in projectiles){
            ProjectileInfo projectileInfo = (ProjectileInfo)proj;
            projectilesByName.Add(projectileInfo.projectileKey, projectileInfo);
        }
    }

    public static ProjectileInfo GetProjectileInfo(string projectileInfoKey){
        InitializeFactory();

        if(projectilesByName.ContainsKey(projectileInfoKey)){
            return projectilesByName[projectileInfoKey];
        }

        Debug.Log($"Projectile {projectileInfoKey} not found!");
        return null;
    }

    public static IEnumerable<string> GetProjectileNames(){
        InitializeFactory();

        return projectilesByName.Keys;
    }
}
