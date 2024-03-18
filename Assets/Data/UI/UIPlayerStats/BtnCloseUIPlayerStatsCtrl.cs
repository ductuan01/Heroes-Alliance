using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseUIPlayerStatsCtrl : BaseButton
{
    protected override void OnClick()
    {
        UIPlayerStatsCtrl.Instance.Toggle();
    }
}
