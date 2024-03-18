using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlashBlast : BaseUISkill
{
    private static UISlashBlast _instance;
    public static UISlashBlast Instance => _instance;
    protected override string _NameSkill => "SlashBlast";

    protected override void Awake()
    {
        if (UISlashBlast._instance != null) Debug.LogError("Only 1 PlayerSkills allow to exist");
        UISlashBlast._instance = this;
    }
}
