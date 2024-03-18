using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDailyGift : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnDailyGift");
        UIManagerCtrl.Instance.UIEventCtrl.BtnEventCtrlToggle();
    }
}
