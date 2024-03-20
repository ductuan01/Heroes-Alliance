using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipInv : PlayerAbstract
{
    private static PlayerEquipInv instance;
    public static PlayerEquipInv Instance => instance;

    [SerializeField] private List<PlayerEquipInvInfo> _playerEquipInvSlots;

    private List<string> _nameEquipInvSlots = new List<string> { "Bottom", "Gloves", "Hat", "Shoes", "Top", "Weapon" };

    protected override void Awake()
    {
        base.Awake();
        if (PlayerEquipInv.instance != null) Debug.LogError("Only 1 PlayerInventory allow to exist");
        PlayerEquipInv.instance = this;
        this.LinkPlayerEquipInvSlots();
    }
    protected override void Start()
    {
        base.LoadComponents();

        this.LoadEquipInvData();
        this.LoadEquipsStats();
        this.PlayerCtrl.PlayerStats.LoadStatsData();
        this.PlayerCtrl.PlayerStats.SetDamage();
        this.PlayerCtrl.PlayerStats.SetTotalHPMP();
        HPMPBarManager.instance.HPMPBarChange(PlayerCtrl.PlayerStats.currentHP, PlayerCtrl.PlayerStats.TotalHP, PlayerCtrl.PlayerStats.currentMP, PlayerCtrl.PlayerStats.TotalMP);
        AbilityManager.Instance.AbilityChange();
    }

    private void CreatePlayerEquipInvSlots()
    {
        this._playerEquipInvSlots.Clear();
        foreach (string nameEquipInvSlot in _nameEquipInvSlots)
        {
            PlayerEquipInvInfo newSlot = new PlayerEquipInvInfo();
            newSlot.nameSlots = nameEquipInvSlot;
            this._playerEquipInvSlots.Add(newSlot);
        }
    }

    public void LinkPlayerEquipInvSlots()
    {
        this.CreatePlayerEquipInvSlots();
        foreach (PlayerEquipInvInfo playerEqiupInvInfo in this._playerEquipInvSlots)
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

    private void LoadEquipInvData()
    {
        string resPath = "GameData/PlayerEquipInv/EquipInvData";
        EquipInvDataSO equipInvDataSO = Resources.Load<EquipInvDataSO>(resPath);
        if (equipInvDataSO == null) return;
        foreach(PlayerEquipInvInfo playerEquipInvInfo in this._playerEquipInvSlots)
        {
            foreach(PlayerEquipInvInfo equipInvData in equipInvDataSO.EquipInvData)
            {
                if(playerEquipInvInfo.nameSlots == equipInvData.nameSlots)
                {
                    playerEquipInvInfo.equipInvInfo.equipProfile = equipInvData.equipInvInfo.equipProfile;
                    playerEquipInvInfo.equipInvInfo.upgradeLevel = equipInvData.equipInvInfo.upgradeLevel;
                }
            }
        }
    }
    public void SaveEquipInvData()
    {
        string resPath = "GameData/PlayerEquipInv/EquipInvData";
        EquipInvDataSO equipInvDataSO = Resources.Load<EquipInvDataSO>(resPath);
        if (equipInvDataSO == null) return;
        foreach (PlayerEquipInvInfo playerEquipInvInfo in this._playerEquipInvSlots)
        {
            foreach (PlayerEquipInvInfo equipInvData in equipInvDataSO.EquipInvData)
            {
                if (playerEquipInvInfo.nameSlots == equipInvData.nameSlots)
                {
                    equipInvData.equipInvInfo.equipProfile = playerEquipInvInfo.equipInvInfo.equipProfile;
                    equipInvData.equipInvInfo.upgradeLevel = playerEquipInvInfo.equipInvInfo.upgradeLevel;
                }
            }
        }
    }
    public void EquipInvDataNewGame()
    {
        string resPath = "GameData/PlayerEquipInv/EquipInvData";
        EquipInvDataSO equipInvDataSO = Resources.Load<EquipInvDataSO>(resPath);
        if (equipInvDataSO == null) return;
        foreach (PlayerEquipInvInfo equipInvData in equipInvDataSO.EquipInvData)
        {
            if (equipInvData.nameSlots == "Weapon")
            {
                equipInvData.equipInvInfo.equipProfile = Resources.Load<EquipProfileSO>("ItemProfiles/Equipment/Warrior/WoodenSword");
                equipInvData.equipInvInfo.upgradeLevel = 0;
            }
            else
            {
                equipInvData.equipInvInfo.equipProfile = null;
                equipInvData.equipInvInfo.upgradeLevel = 0;
            }    
        }
        this.LoadEquipInvData();
        this.LoadEquipsStats();
        PlayerEquipInvSlotsCtrl.Instance.LoadEquipImage();
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

        foreach(PlayerEquipInvInfo playerEquipInv in this._playerEquipInvSlots)
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
