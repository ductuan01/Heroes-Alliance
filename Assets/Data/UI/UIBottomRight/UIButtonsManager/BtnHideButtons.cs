using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnHideButtons : BaseButton
{
    protected override void OnClick()
    {
        UIButtonsManagerCtrl.Instance.BtnHideButtonsTogle();
    }
}
