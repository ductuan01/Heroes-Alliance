using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUse : BaseButton
{
    protected override void OnClick()
    {
        UIInventoryCtrl.Instance.LoadUseInv();
    }
}
