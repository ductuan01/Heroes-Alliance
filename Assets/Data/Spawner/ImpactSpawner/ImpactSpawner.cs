using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSpawner : Spawner
{
    private static ImpactSpawner instance;
    public static ImpactSpawner Instance => instance;

    public static string impact_1 = "Impact_1";
    public static string impact_2 = "Impact_2";

    protected override void Awake()
    {
        base.Awake();
        if (ImpactSpawner.instance != null) Debug.LogError("Only 1 ImpactSpawner allow to exist");
        ImpactSpawner.instance = this;
    }
}
