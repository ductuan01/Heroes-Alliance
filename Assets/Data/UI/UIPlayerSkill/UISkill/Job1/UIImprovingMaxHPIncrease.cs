using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIImprovingMaxHPIncrease : BaseUISkill
{
    private static UIImprovingMaxHPIncrease _instance;
    public static UIImprovingMaxHPIncrease Instance => _instance;
    protected override string NameSkill => "ImprovingMaxHPIncrease";

    protected override void Awake()
    {
        if (UIImprovingMaxHPIncrease._instance != null) Debug.LogError("Only 1 UIImprovingMaxHPIncrease allow to exist");
        UIImprovingMaxHPIncrease._instance = this;
    }
}
