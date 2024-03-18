using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRaiseSlashBlastLevel : BaseButton
{
    protected override void OnClick()
    {
        SlashBlast.Instance.RaiseSkillLevel();
    }
}
