using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnNothing : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnNothing");
        UIManagerCtrl.Instance.UICommunityCtrl.BtnCommunityCtrlToggle();
    }
}
