using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEventList : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnEventList");
        UIManagerCtrl.Instance.UIEventCtrl.BtnEventCtrlToggle();
    }
}
