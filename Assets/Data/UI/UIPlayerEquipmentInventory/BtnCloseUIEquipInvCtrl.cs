using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseUIEquipInvCtrl : BaseButton
{
    protected override void OnClick()
    {
        UIPlayerEquipmentInventoryCtrl.Instance.Toggle();
    }
}
