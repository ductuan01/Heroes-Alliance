using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRaiseWarLeap : BaseButton
{
    protected override void OnClick()
    {
        WarLeap.Instance.RaiseSkillLevel();
    }
}
