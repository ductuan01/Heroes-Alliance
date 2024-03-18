using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseKeyBindings : BaseButton
{
    protected override void OnClick()
    {
        UIKeyBindingsCtrl.Instance.KeyBindingsToggle();
    }
}