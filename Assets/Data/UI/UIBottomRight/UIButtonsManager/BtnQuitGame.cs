using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnQuitGame : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnQuitGame");
        Application.Quit();
        /*        PlayerEquipInv.Instance.SaveEquipInvData();
                PlayerInventory.Instance.SaveItemsData();
                PlayerLevel.Instance.SaveLevelData();
                PlayerSkills.Instance.SaveSkillsData();
                PlayerStats.Instance.SaveStatsData();
                UIManagerCtrl.Instance.UIMenuCtrl.BtnMenuCtrlToggle();*/
    }
}
