using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCharacterCtrl : BaseButton
{
    protected override void OnClick()
    {
        if (UIManagerCtrl.Instance.UICharacterCtrl.IsOpen == true)
        {
            UIManagerCtrl.Instance.UICharacterCtrl.BtnCharacterCtrlToggle();
        }
        else
        {
            UIManagerCtrl.Instance.HideAllUICtrl();
            UIManagerCtrl.Instance.UICharacterCtrl.BtnCharacterCtrlToggle();
        }
    }
}
