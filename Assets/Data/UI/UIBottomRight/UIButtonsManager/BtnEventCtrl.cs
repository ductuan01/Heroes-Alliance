using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEventCtrl : BaseButton
{
    protected override void OnClick()
    {
        if (UIManagerCtrl.Instance.UIEventCtrl.IsOpen == true)
        {
            UIManagerCtrl.Instance.UIEventCtrl.BtnEventCtrlToggle();
        }
        else
        {
            UIManagerCtrl.Instance.HideAllUICtrl();
            UIManagerCtrl.Instance.UIEventCtrl.BtnEventCtrlToggle();
        }
    }
}
