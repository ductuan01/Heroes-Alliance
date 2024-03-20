using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointManager : SecondMonoBehaviour
{
    public delegate void SkillPointChangeHandler(int point);

    public event SkillPointChangeHandler OnSkillPointChange;

    public delegate void SkillLevelChangeHandler(int level);

    public event SkillLevelChangeHandler OnSkillLevelChange;

    private static SkillPointManager _instance;
    public static SkillPointManager Instance => _instance;

    protected override void Awake()
    {
        base.Awake();
        if (SkillPointManager._instance != null) Debug.LogError("Only 1 SkillPointManager allow to exist");
        SkillPointManager._instance = this;
    }

    public void SkillPointChange(int point)
    {
        OnSkillPointChange?.Invoke(point);
    }

    public void SkillLevelChange(int level)
    {
        OnSkillLevelChange?.Invoke(level);
    }
}
