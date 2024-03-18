using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMPBarManager : SecondMonoBehaviour
{
    public delegate void HPMPBarChangeHandler();
    public event HPMPBarChangeHandler OnHPMPBarChange;

    private static HPMPBarManager _instance;
    public static HPMPBarManager instance => _instance;

    protected override void Awake()
    {
        base.Awake();
        if (HPMPBarManager._instance != null) Debug.LogError("Only 1 HPMPBarManager allow to exist");
        HPMPBarManager._instance = this;
    }

    public void HPMPBarChange(int currentHP, int totalHP, int currentMP, int totalMP)
    {
        OnHPMPBarChange?.Invoke();
    }
}
