using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Spawner
{
    private static MonsterSpawner _instance;
    public static MonsterSpawner Instance => _instance;

    public static string skeletonPikeman = "SkeletonPikeman";

    protected override void Awake()
    {
        base.Awake();
        if (MonsterSpawner._instance != null) Debug.LogError("Only 1 MonsterSpawner allow to exist");
        MonsterSpawner._instance = this;
    }
}
