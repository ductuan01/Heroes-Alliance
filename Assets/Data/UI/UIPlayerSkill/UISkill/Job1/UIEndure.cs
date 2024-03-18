using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndure : BaseUISkill
{
    private static UIEndure _instance;
    public static UIEndure Instance => _instance;
    protected override string _NameSkill => "Endure";

    protected override void Awake()
    {
        if (UIEndure._instance != null) Debug.LogError("Only 1 UIEndure allow to exist");
        UIEndure._instance = this;
    }
}
