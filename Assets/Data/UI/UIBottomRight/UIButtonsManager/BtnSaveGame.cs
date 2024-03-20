using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSaveGame : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnSaveGame");
        PlayerEquipInv.Instance.SaveEquipInvData();
        PlayerInventory.Instance.SaveItemsData();
        PlayerLevel.Instance.SaveLevelData();
        PlayerSkills.Instance.SaveSkillsData();
        PlayerStats.Instance.SaveStatsData();
        UIManagerCtrl.Instance.UIMenuCtrl.BtnMenuCtrlToggle();
    }
}
