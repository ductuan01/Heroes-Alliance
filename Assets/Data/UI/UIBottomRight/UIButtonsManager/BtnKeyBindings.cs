using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnKeyBindings : BaseButton
{
    protected override void OnClick()
    {
        UIKeyBindingsCtrl.Instance.KeyBindingsToggle();
        UIManagerCtrl.Instance.UISettingCtrl.BtnSettingCtrlToggle();
    }
}
