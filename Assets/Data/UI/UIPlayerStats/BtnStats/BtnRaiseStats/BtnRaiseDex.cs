using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRaiseDex : BaseButton
{
    protected override void OnClick()
    {
        PlayerStats.Instance.RaiseDEXAbility();
    }
}
