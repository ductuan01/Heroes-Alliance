using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipInformation
{
    public EquipProfileSO equipProfile = null;
    public int upgradeLevel = 0;

    public void SetEquipInfo(EquipInformation equipInfo)
    {
        this.equipProfile = equipInfo.equipProfile;
        this.upgradeLevel = equipInfo.upgradeLevel;
    }

    public void SetEquipInfoNull()
    {
        this.equipProfile = null;
        this.upgradeLevel = 0;
    }
}
