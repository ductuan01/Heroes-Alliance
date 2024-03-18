using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuitGame : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnQuitGame");
        UIManagerCtrl.Instance.UIMenuCtrl.BtnMenuCtrlToggle();
    }
}
