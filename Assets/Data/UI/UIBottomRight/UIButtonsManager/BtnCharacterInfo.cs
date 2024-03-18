using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCharacterInfo : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnCharacterInfo");
        UIManagerCtrl.Instance.UICharacterCtrl.BtnCharacterCtrlToggle();
    }
}
