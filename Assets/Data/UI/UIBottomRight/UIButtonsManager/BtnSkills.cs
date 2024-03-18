using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSkills : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnSkills");
        UIPlayerSkillCtrl.Instance.Toggle();
        UIManagerCtrl.Instance.UICharacterCtrl.BtnCharacterCtrlToggle();
    }
}
