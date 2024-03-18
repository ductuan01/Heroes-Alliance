using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnEtc : BaseButton
{
    protected override void OnClick()
    {
        UIInventoryCtrl.Instance.LoadEtcInv();
    }
}
