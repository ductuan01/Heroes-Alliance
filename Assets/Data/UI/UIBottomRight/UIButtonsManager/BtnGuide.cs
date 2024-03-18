using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnGuide : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnGuide");
        UIManagerCtrl.Instance.UICharacterCtrl.BtnCharacterCtrlToggle();
    }
}
