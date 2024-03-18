using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseUIInventoryCtrl : BaseButton
{
    protected override void OnClick()
    {
        UIInventoryCtrl.Instance.Toggle();
    }
}
