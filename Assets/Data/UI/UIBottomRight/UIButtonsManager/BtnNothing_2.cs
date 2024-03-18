using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnNothing_2 : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnNothing_2");
        UIManagerCtrl.Instance.UICommunityCtrl.BtnCommunityCtrlToggle();
    }
}
