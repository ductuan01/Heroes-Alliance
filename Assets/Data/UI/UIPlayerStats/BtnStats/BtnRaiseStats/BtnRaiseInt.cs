using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRaiseInt : BaseButton
{
    protected override void OnClick()
    {
        PlayerStats.Instance.RaiseINTAbility();
    }
}
