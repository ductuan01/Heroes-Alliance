using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEquipment : BaseButton
{
    protected override void OnClick()
    {
        UIPlayerEquipmentInventoryCtrl.Instance.Toggle();
        UIManagerCtrl.Instance.UICharacterCtrl.BtnCharacterCtrlToggle();
    }
}
