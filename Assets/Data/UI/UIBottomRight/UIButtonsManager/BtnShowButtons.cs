using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShowButtons : BaseButton
{
    protected override void OnClick()
    {
        UIButtonsManagerCtrl.Instance.BtnShowButtonsTogle();
    }
}
