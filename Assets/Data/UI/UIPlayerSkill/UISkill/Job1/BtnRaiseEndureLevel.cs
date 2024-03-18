using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRaiseEndureLevel : BaseButton
{
    protected override void OnClick()
    {
        Endure.Instance.RaiseLevelSkill();
    }
}
