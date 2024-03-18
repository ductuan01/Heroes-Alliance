using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEffectSpawner : Spawner
{
    private static LevelEffectSpawner _instance;
    public static LevelEffectSpawner instance => _instance;

    public static string levelEffect_1 = "LevelEffect_1";

    protected override void Awake()
    {
        base.Awake();
        if (LevelEffectSpawner._instance != null) Debug.LogError("Only 1 LevelEffectSpawner allow to exist");
        LevelEffectSpawner._instance = this;
    }
}
