using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : SecondMonoBehaviour
{
    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;

    public delegate void LevelChangeHandler(int level);
    public event LevelChangeHandler OnLevelChange;

    private static ExperienceManager _instance;
    public static ExperienceManager Instance => _instance;

    protected override void Awake()
    {
        base.Awake();
        if (ExperienceManager._instance != null) Debug.LogError("Only 1 ExperienceManager allow to exist");
        ExperienceManager._instance = this;
    }

    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }

    public void LevelUp(int level)
    {
        OnLevelChange?.Invoke(level);
    }
}
