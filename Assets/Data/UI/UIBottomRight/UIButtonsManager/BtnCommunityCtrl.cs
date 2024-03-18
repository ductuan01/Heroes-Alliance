using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCommunityCtrl : BaseButton
{
    protected override void OnClick()
    {
        if (UIManagerCtrl.Instance.UICommunityCtrl.IsOpen == true)
        {
            UIManagerCtrl.Instance.UICommunityCtrl.BtnCommunityCtrlToggle();
        }
        else
        {
            UIManagerCtrl.Instance.HideAllUICtrl();
            UIManagerCtrl.Instance.UICommunityCtrl.BtnCommunityCtrlToggle();
        }
    }
}
