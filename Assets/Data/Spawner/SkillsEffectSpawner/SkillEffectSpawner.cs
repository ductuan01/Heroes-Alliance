using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectSpawner : Spawner
{
    private static SkillEffectSpawner instance;
    public static SkillEffectSpawner Instance => instance;

    public static string attack = "Attack";
    public static string slashblast = "SlashBlast";

    protected override void Awake()
    {
        base.Awake();
        if (SkillEffectSpawner.instance != null) Debug.LogError("Only 1 SlashSpawner allow to exist");
        SkillEffectSpawner.instance = this;
    }
}
