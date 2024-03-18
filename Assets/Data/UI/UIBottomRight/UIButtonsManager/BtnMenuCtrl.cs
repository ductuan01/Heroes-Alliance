using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnMenuCtrl : BaseButton
{
    protected override void OnClick()
    {
        if(UIManagerCtrl.Instance.UIMenuCtrl.IsOpen == true)
        {
            UIManagerCtrl.Instance.UIMenuCtrl.BtnMenuCtrlToggle();
        }
        else
        {
            UIManagerCtrl.Instance.HideAllUICtrl();
            UIManagerCtrl.Instance.UIMenuCtrl.BtnMenuCtrlToggle();
        }
    }
}
