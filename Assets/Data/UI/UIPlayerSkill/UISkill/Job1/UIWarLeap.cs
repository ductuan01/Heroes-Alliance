using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWarLeap : BaseUISkill
{
    private static UIWarLeap _instance;
    public static UIWarLeap Instance => _instance;
    protected override string _NameSkill => "WarLeap";

    protected override void Awake()
    {
        if (UIWarLeap._instance != null) Debug.LogError("Only 1 UIWarLeap allow to exist");
        UIWarLeap._instance = this;
    }
}
