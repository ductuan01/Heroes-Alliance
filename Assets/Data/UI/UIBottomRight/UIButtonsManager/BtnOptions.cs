using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOptions : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnOptions");
        UIManagerCtrl.Instance.UISettingCtrl.BtnSettingCtrlToggle();
    }
}
