using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRaiseStr : BaseButton
{
    protected override void OnClick()
    {
        PlayerStats.Instance.RaiseSTRAbility();
    }
}
