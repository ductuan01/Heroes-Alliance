using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : SecondMonoBehaviour
{
    public delegate void AbilityChangeHandler();
    public event AbilityChangeHandler OnAbilityChange;

    private static AbilityManager _instance;
    public static AbilityManager Instance => _instance;

    protected override void Awake()
    {
        base.Awake();
        if (AbilityManager._instance != null) Debug.LogError("Only 1 AbilityManager allow to exist");
        AbilityManager._instance = this;
    }

    public void AbilityChange()
    {
        OnAbilityChange?.Invoke();
    }
}
