using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSettingCtrl : BaseButton
{
    protected override void OnClick()
    {
        if (UIManagerCtrl.Instance.UISettingCtrl.IsOpen == true)
        {
            UIManagerCtrl.Instance.UISettingCtrl.BtnSettingCtrlToggle();
        }
        else
        {
            UIManagerCtrl.Instance.HideAllUICtrl();
            UIManagerCtrl.Instance.UISettingCtrl.BtnSettingCtrlToggle();
        }
    }
}
