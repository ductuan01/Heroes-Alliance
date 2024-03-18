using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOKToRevive : BaseButton
{
    protected override void OnClick()
    {
        PlayerStats.Instance.OnDead();
    }
}
