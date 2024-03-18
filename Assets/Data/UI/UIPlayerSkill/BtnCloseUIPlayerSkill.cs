using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseUIPlayerSkill : BaseButton
{
    protected override void OnClick()
    {
        UIPlayerSkillCtrl.Instance.Toggle();
    }
}
