using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEquip : BaseButton
{
    protected override void OnClick()
    {
        UIInventoryCtrl.Instance.LoadEquipInv();
    }
}
