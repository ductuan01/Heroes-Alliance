using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : PlayerAbstract
{
    private static PlayerInventory _instance;
    public static PlayerInventory Instance => _instance;

    // ==== Equip ====
    [SerializeField] private List<EquipInformation> _equipInventory;
    public List<EquipInformation> EquipInventory => _equipInventory;
    [SerializeField] private int _equipInventorySlot = 48;

    // ==== Use ====
    [SerializeField] private List<UseInformation> _useInventory;
    public List<UseInformation> UseInventory => _useInventory;
    [SerializeField] private int _useInventorySlot = 48;

    // ==== Etc ====
    [SerializeField] private List<EtcInformation> _etcInventory;
    public List<EtcInformation> EtcInventory => _etcInventory;
    [SerializeField] private int _etcInventorySlot = 48;

    // ==== NTD ==== // coin name of the game
    [SerializeField] private int _ntd; 
    public int Ntd => _ntd;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerInventory._instance != null) Debug.LogError("Only 1 PlayerInventory allow to exist");
        PlayerInventory._instance = this;

        this.CreateEmptyEquip();
        this.CreateEmptyUses();
        this.CreateEmptyEtcs();
    }

    private void Update()
    {
        // for KeyBindings
        if (InputManager.Instance.GetKeyDown(KeybindingActions.BluePotion_1))
        {
            this.UseRecoveryItem(UseCode.BluePotion_1);
        }

        if (InputManager.Instance.GetKeyDown(KeybindingActions.RedPotion_1))
        {
            this.UseRecoveryItem(UseCode.RedPotion_1);
        }
    }

    private void CreateEmptyEquip()
    {
        this._equipInventory.Clear();
        for (int i = 0; i < this._equipInventorySlot; i++)
        {
            _equipInventory.Add(new EquipInformation());
        }
    }

    private void CreateEmptyUses()
    {
        this._useInventory.Clear();
        for (int i = 0; i < this._useInventorySlot; i++)
        {
            _useInventory.Add(new UseInformation());
        }
    }
    private void CreateEmptyEtcs()
    {
        this._etcInventory.Clear();
        for (int i = 0; i < this._etcInventorySlot; i++)
        {
            _etcInventory.Add(new EtcInformation());
        }
    }

    // ============================= NTD ===================================
    public bool RaiseNTD(int amount)
    {
        this._ntd += amount;
        InventoryManager.Instance.NTDChange(this._ntd);
        return true;
    }

    public bool ReduceNTD(int amount)
    {
        this._ntd += amount;
        InventoryManager.Instance.NTDChange(this._ntd);
        return true;
    }

    protected virtual bool IsInventoryFull(int itemCount ,int maxSlot)
    {
        return itemCount >= maxSlot;
    }
    // ============================= NTD ===================================

    // ============================= Equip ===================================
    public virtual bool AddEquipToInv(EquipInformation equipmentInformation)
    {
        EquipInformation equip = this.FindEmptyEquipSlot(equipmentInformation);
        if (equip == null) return false;
        InventoryManager.Instance.InventoryChange();
        return true;
    }
    protected virtual EquipInformation FindEmptyEquipSlot(EquipInformation equipmentInformation)
    {
        foreach (EquipInformation equip in this._equipInventory)
        {
            if (equip.equipProfile == null)
            {
                equip.equipProfile = equipmentInformation.equipProfile;
                equip.upgradeLevel = equipmentInformation.upgradeLevel;
                return equip;
            }
        }
        return null;
    }

    public void SwapEquip(int index1, int index2)
    {
        EquipInformation temp = this._equipInventory[index1];
        this._equipInventory[index1] = this._equipInventory[index2];
        this._equipInventory[index2] = temp;
    }
    public void SwapEquip(EquipInformation equip1, EquipInformation equip2)
    {
        int index1 = _equipInventory.IndexOf(equip1);
        int index2 = _equipInventory.IndexOf(equip2);

        EquipInformation temp = this._equipInventory[index1];
        this._equipInventory[index1] = this._equipInventory[index2];
        this._equipInventory[index2] = temp;
    }
    public void SwapEquipData(EquipInformation equip1, EquipInformation equip2)
    {
        EquipInformation temp = new EquipInformation();
        temp.SetEquipInfo(equip1);
        equip1.SetEquipInfo(equip2);
        equip2.SetEquipInfo(temp);
    }
    public EquipInformation InfoEquipDrop(EquipInformation equip)
    {
        int index = _equipInventory.IndexOf(equip);
        EquipInformation EquipDrop = this._equipInventory[index];
        EquipInformation InfoEquipDrop = new EquipInformation();

        InfoEquipDrop.SetEquipInfo(EquipDrop);
        EquipDrop.SetEquipInfoNull();

        return InfoEquipDrop;
    }

    // ============================= Equip ===================================


    // ============================= USE ===================================
    public virtual bool AddUseToInv(UseInformation useInformation)
    {
        if (useInformation == null) return false;
        int addRemain = useInformation.Amount;
        int newCount;
        int itemMaxStack = useInformation.maxStack;
        int addMore;

        for (int i = 0; i < this._useInventorySlot; i++)
        {
            UseInformation useNotFullStack = this.GetUseNotFullStack(useInformation.useProfile.useCode);

            if (useNotFullStack == null)
            {
                useNotFullStack = this.FindEmptyUseSlot(useInformation);
                if (useNotFullStack == null) return false;
            }

            newCount = useNotFullStack.Amount + addRemain;

            if (newCount > itemMaxStack)
            {
                addMore = itemMaxStack - useNotFullStack.Amount;
                newCount = addMore + useNotFullStack.Amount;
                addRemain -= addMore;
            }
            else
            {
                addRemain -= newCount;
            }

            useNotFullStack.Amount = newCount;
            if (addRemain < 1) break;
        }
        InventoryManager.Instance.InventoryChange();
        InventoryManager.Instance.UseAmountChange(useInformation.useProfile.useCode);
        return true;
    }

    protected virtual UseInformation GetUseNotFullStack(UseCode useCode)
    {
        foreach (UseInformation use in this._useInventory)
        {
            if (use.useProfile == null) continue;
            if (use.useProfile.useCode != useCode) continue;
            if (this.UseIsFullStack(use)) continue;
            return use;
        }
        return null;
    }

    protected virtual bool UseIsFullStack(UseInformation useInfomation)
    {
        if (useInfomation == null) return true;
        return useInfomation.Amount >= useInfomation.maxStack;
    }

    protected virtual UseInformation FindEmptyUseSlot(UseInformation useInfomation)
    {
        foreach (UseInformation use in this._useInventory)
        {
            if (use.useProfile == null && use.Amount == 0)
            {
                use.useProfile = useInfomation.useProfile;
                use.maxStack = useInfomation.useProfile.defaultMaxStack;
                return use;
            }
        }
        return null;
    }

    public void SwapUse(UseInformation use1, UseInformation use2)
    {
        int index1 = _useInventory.IndexOf(use1);
        int index2 = _useInventory.IndexOf(use2);

        UseInformation temp = this._useInventory[index1];
        this._useInventory[index1] = this._useInventory[index2];
        this._useInventory[index2] = temp;
    }

    public virtual void UseRecoveryItem(UseCode useCode)
    {
        UseInformation useItem = this.FindUseByCode(useCode);
        if (!ReduceUseInInv(useItem)) return;
        if (useItem.useProfile == null) return;
        PlayerCtrl.Instance.PlayerStats.RiseHP(useItem.useProfile.RecoversHP);
        PlayerCtrl.Instance.PlayerStats.RiseMP(useItem.useProfile.RecoversMP);
        InventoryManager.Instance.InventoryChange();
        InventoryManager.Instance.UseAmountChange(useCode);

    }

    private bool ReduceUseInInv(UseInformation useItem)
    {
        if (useItem == null) return false;
        if (useItem.Amount <= 0) return false;
        useItem.Amount -= 1; // reduce 1;
        return true;
    }

    private UseInformation FindUseByCode(UseCode useCode)
    {
        foreach (UseInformation use in this._useInventory)
        {
            if (use.useProfile == null) continue;
            if (use.useProfile.useCode != useCode) continue;
            if (use.useProfile.useCode == useCode) return use;
        }
        return null;
    }

    public virtual int GetTotalAmountOfUseItem(UseCode useCode)
    {
        int total = 0;
        foreach (UseInformation use in this._useInventory)
        {
            if (use.useProfile == null) continue;
            if (use.useProfile.useCode != useCode) continue;
            if (use.useProfile.useCode == useCode)
            {
                total += use.Amount;
            }
        }
        return total;
    }

    ///
    public UseInformation InfoUseDrop(UseInformation use)
    {
        int index = this._useInventory.IndexOf(use);
        UseInformation UseDrop = this._useInventory[index];
        UseInformation InfoUseDrop = new UseInformation();

        InfoUseDrop.SetUseInfo(UseDrop);
        UseDrop.SetUseInfoNull();

        return InfoUseDrop;
    }

    public UseInformation InfoUseDrop(UseInformation use, int amountDrop)
    {
        int index = this._useInventory.IndexOf(use);
        UseInformation UseDrop = this._useInventory[index];
        UseDrop.Amount -= amountDrop;

        UseInformation InfoUseDrop = new UseInformation
        {
            useProfile = UseDrop.useProfile,
            Amount = amountDrop,
            maxStack = UseDrop.maxStack
        };

        if (UseDrop.Amount == 0) UseDrop.SetUseInfoNull();

        return InfoUseDrop;
    }
    // ============================= USE ===================================

    // ============================= ETC ===================================
    public virtual bool AddEtcToInv(EtcInformation etcInformation)
    {
        if (etcInformation == null) return false;

        int addRemain = etcInformation.Amount;
        int newCount;
        int itemMaxStack = etcInformation.maxStack;
        int addMore;

        for (int i = 0; i < this._etcInventorySlot; i++)
        {
            EtcInformation etcNotFullStack = this.GetEtcNotFullStack(etcInformation.etcProfile.etcCode);

            if (etcNotFullStack == null)
            {
                etcNotFullStack = this.FindEmptyEtcSlot(etcInformation);
                if (etcNotFullStack == null) return false;
            }

            newCount = etcNotFullStack.Amount + addRemain;

            if (newCount > itemMaxStack)
            {
                addMore = itemMaxStack - etcNotFullStack.Amount;
                newCount = addMore + etcNotFullStack.Amount;
                addRemain -= addMore;
            }
            else
            {
                addRemain -= newCount;
            }

            etcNotFullStack.Amount = newCount;
            if (addRemain < 1) break;
        }
        InventoryManager.Instance.InventoryChange();
        return true;
    }

    protected virtual EtcInformation GetEtcNotFullStack(EtcCode ectCode)
    {
        foreach (EtcInformation etc in this._etcInventory)
        {
            if (etc.etcProfile == null) continue;
            if (etc.etcProfile.etcCode != ectCode) continue;
            if (this.EtcIsFullStack(etc)) continue;
            return etc;
        }
        return null;
    }
    
    protected virtual bool EtcIsFullStack(EtcInformation etcInfomation)
    {
        if (etcInfomation == null) return true;
        return etcInfomation.Amount >= etcInfomation.maxStack;
    }

    protected virtual EtcInformation FindEmptyEtcSlot(EtcInformation etcInfomation)
    {
        foreach (EtcInformation etc in this._etcInventory)
        {
            if (etc.etcProfile == null && etc.Amount == 0)
            {
                etc.etcProfile = etcInfomation.etcProfile;
                etc.maxStack = etcInfomation.etcProfile.defaultMaxStack;
                return etc;
            }
        }
        return null;
    }

    public void SwapEtc(EtcInformation etc1, EtcInformation etc2)
    {
        int index1 = _etcInventory.IndexOf(etc1);
        int index2 = _etcInventory.IndexOf(etc2);

        EtcInformation temp = this._etcInventory[index1];
        this._etcInventory[index1] = this._etcInventory[index2];
        this._etcInventory[index2] = temp;
    }

    public EtcInformation InfoEtcDrop(EtcInformation etc)
    {
        int index = this._etcInventory.IndexOf(etc);
        EtcInformation EtcDrop = this._etcInventory[index];
        EtcInformation InfoEtcDrop = new EtcInformation();

        InfoEtcDrop.SetEtcInfo(EtcDrop);
        EtcDrop.SetEtcInfoNull();

        return InfoEtcDrop;
    }

    public EtcInformation InfoEtcDrop(EtcInformation etc, int amountDrop)
    {
        int index = this._etcInventory.IndexOf(etc);
        EtcInformation EtcDrop = this._etcInventory[index];
        EtcDrop.Amount -= amountDrop;

        EtcInformation InfoEtcDrop = new EtcInformation
        {
            etcProfile = EtcDrop.etcProfile,
            Amount = amountDrop,
            maxStack = EtcDrop.maxStack
        };

        if (EtcDrop.Amount == 0) EtcDrop.SetEtcInfoNull();

        return InfoEtcDrop;
    }
    // ============================= ETC ===================================
}
