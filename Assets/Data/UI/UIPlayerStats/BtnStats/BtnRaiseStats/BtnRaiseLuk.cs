using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRaiseLuk : BaseButton
{
    protected override void OnClick()
    {
        PlayerStats.Instance.RaiseLUKAbility();
    }
}
