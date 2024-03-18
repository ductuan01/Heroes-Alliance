using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnStats : BaseButton
{
    protected override void OnClick()
    {
        UIPlayerStatsCtrl.Instance.Toggle();
        UIManagerCtrl.Instance.UICharacterCtrl.BtnCharacterCtrlToggle();
    }
}
