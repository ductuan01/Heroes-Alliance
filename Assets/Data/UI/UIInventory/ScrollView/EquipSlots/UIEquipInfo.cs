using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipInfo : MonoBehaviour
{
    [SerializeField] private EquipInformation _equipInformation;
    public EquipInformation equipInformation => _equipInformation;

    public virtual void LinkEquipInfo(EquipInformation equipmentInformation)
    {
        this._equipInformation = equipmentInformation;
    }

    public virtual bool SetEquipInfo(EquipInformation equipmentInformation)
    {
        if (equipmentInformation == null) return false;
        if (equipmentInformation.equipProfile == null) return false;
        if (equipmentInformation.upgradeLevel < 0) return false;
        Debug.Log(equipmentInformation.equipProfile);
        Debug.Log(equipmentInformation.upgradeLevel);
        this._equipInformation.equipProfile = equipmentInformation.equipProfile;
        this._equipInformation.upgradeLevel = equipmentInformation.upgradeLevel;
        return true;
    }
    
    public virtual void SetEquipInforNull()
    {
        this._equipInformation.equipProfile = null;
        this._equipInformation.upgradeLevel = 0;
    }
}
