using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnInventory : BaseButton
{
    protected override void OnClick()
    {
        UIInventoryCtrl.Instance.Toggle();
        UIManagerCtrl.Instance.UICharacterCtrl.BtnCharacterCtrlToggle();
    }
}
