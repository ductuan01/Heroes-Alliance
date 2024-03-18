using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipInv : PlayerAbstract
{
    private static PlayerEquipInv instance;
    public static PlayerEquipInv Instance => instance;

    [SerializeField] private List<PlayerEquipInvInfo> PlayerEquipInvSlots;

    private List<string> _nameEquipInvSlots = new List<string> { "Bottom", "Gloves", "Hat", "Shoes", "Top", "Weapon" };

    protected override void Awake()
    {
        base.Awake();
        if (PlayerEquipInv.instance != null) Debug.LogError("Only 1 PlayerInventory allow to exist");
        PlayerEquipInv.instance = this;
        this.LinkPlayerEquipInvSlots();
    }

    private void CreatePlayerEquipInvSlots()
    {
        this.PlayerEquipInvSlots.Clear();
        foreach (string nameEquipInvSlot in _nameEquipInvSlots)
        {
            PlayerEquipInvInfo newSlot = new PlayerEquipInvInfo();
            newSlot.nameSlots = nameEquipInvSlot;
            this.PlayerEquipInvSlots.Add(newSlot);
        }
    }

    public void LinkPlayerEquipInvSlots()
    {
        this.CreatePlayerEquipInvSlots();
        foreach (PlayerEquipInvInfo playerEqiupInvInfo in this.PlayerEquipInvSlots)
        {
            foreach (EquipSlot equipInvSlot in PlayerEquipInvSlotsCtrl.Instance.equipSlots)
            {
                if (equipInvSlot.name == playerEqiupInvInfo.nameSlots)
                {
                    playerEqiupInvInfo.equipInvInfo = equipInvSlot.uiEquipInfo.equipInformation;
                    break;
                }
            }
            continue;
        }
    }

    public void LoadEquipsStats()
    {
        int STR = 0;
        int DEX = 0;
        int INT = 0;
        int LUK = 0;
        int MaxHP = 0;
        int MaxMP = 0;
        int AttPower = 0;
        int MattPower = 0;

        foreach(PlayerEquipInvInfo playerEquipInv in this.PlayerEquipInvSlots)
        {
            EquipInformation equipInfo = playerEquipInv.equipInvInfo;
            if (equipInfo.equipProfile == null) continue;
            STR += equipInfo.equipProfile.STR * (equipInfo.upgradeLevel + 1);
            DEX += equipInfo.equipProfile.DEX * (equipInfo.upgradeLevel + 1);
            INT += equipInfo.equipProfile.INT * (equipInfo.upgradeLevel + 1);
            LUK += equipInfo.equipProfile.LUK * (equipInfo.upgradeLevel + 1);
            MaxHP += equipInfo.equipProfile.MaxHP * (equipInfo.upgradeLevel + 1);
            MaxMP += equipInfo.equipProfile.MaxMP * (equipInfo.upgradeLevel + 1);
            AttPower += equipInfo.equipProfile.AttPower * (equipInfo.upgradeLevel + 1);
            MattPower += equipInfo.equipProfile.MattPower * (equipInfo.upgradeLevel + 1);
        }

        PlayerCtrl.PlayerStats.SetEtcStats(STR, DEX, INT, LUK, MaxHP, MaxMP, AttPower, MattPower);
    }
}
