using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRaiseImprovingMaxHPIncrease : BaseButton
{
    protected override void OnClick()
    {
        ImprovingMaxHPIncrease.Instance.RaiseSkillLevel();
    }
}
