using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnNewGame : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("BtnNewGame");
        PlayerEquipInv.Instance.EquipInvDataNewGame();
        PlayerInventory.Instance.ItemsDataNewGame();
        PlayerLevel.Instance.LevelDataNewGame();
        PlayerSkills.Instance.SkillsDataNewGame();
        PlayerStats.Instance.StatsDataNewGame();
        InputManager.Instance.KeyBindingsNewGame();
        MonsterTutorial.Instance.SetMonsterTutorialActive();
        UIManagerCtrl.Instance.UIMenuCtrl.BtnMenuCtrlToggle();
        this.DespawnItems();
    }

    private void DespawnItems()
    {
        EquipSpawner.Instance.DespawnAllInHolder();
        UseSpawner.Instance.DespawnAllInHolder();
        EtcSpawner.Instance.DespawnAllInHolder();
        NTDSpawner.Instance.DespawnAllInHolder();
    }
}
