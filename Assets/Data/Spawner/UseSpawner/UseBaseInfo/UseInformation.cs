using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UseInformation
{
    public UseProfileSO useProfile;
    public int Amount = 0;
    public int maxStack = 3;

    public bool useItem()
    {
        if (useProfile == null) return false;
        UseType useType = useProfile.useType;
        switch (useType)
        {
            case UseType.RecoveryItem:
                RecoveryItem();
                break;
            case UseType.Scroll:
                Debug.Log("Option 2 selected.");
                break;
            default:
                Debug.Log("Invalid option selected.");
                break;
        }
        InventoryManager.Instance.InventoryChange();
        InventoryManager.Instance.UseAmountChange(this.useProfile.useCode);
        if (this.Amount == 0) SetUseInfoNull();
        return true;
    }

    private bool RecoveryItem()
    {
        if (this.Amount <= 0) return false;
        this.Amount -= 1;
        PlayerStats.Instance.RiseHP(useProfile.RecoversHP);
        PlayerStats.Instance.RiseMP(useProfile.RecoversMP);
        return true;
    }

    public void SetUseInfo(UseInformation useInfo)
    {
        this.useProfile = useInfo.useProfile;
        this.Amount = useInfo.Amount;
        this.maxStack = useInfo.maxStack;
    }

    public void SetUseInfoNull()
    {
        this.useProfile = null;
        this.Amount = 0;
        this.maxStack = 0;
    }
}
