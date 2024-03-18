using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSaveGame : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnSaveGame");
        UIManagerCtrl.Instance.UIMenuCtrl.BtnMenuCtrlToggle();
    }
}
