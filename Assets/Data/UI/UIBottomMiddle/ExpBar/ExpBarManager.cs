using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBarManager : SecondMonoBehaviour
{
    public delegate void ExpBarChangeHandler();
    public event ExpBarChangeHandler OnExpBarChange;

    private static ExpBarManager _instance;
    public static ExpBarManager instance => _instance;

    protected override void Awake()
    {
        base.Awake();
        if (ExpBarManager._instance != null) Debug.LogError("Only 1 ExpBarManager allow to exist");
        ExpBarManager._instance = this;
    }

    public void ExpBarChange()
    {
        OnExpBarChange?.Invoke();
    }
}
