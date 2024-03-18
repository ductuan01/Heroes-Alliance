using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipBaseInfo : EquipAbstract
{
    [SerializeField] private EquipProfileSO _equipProfile;
    public EquipProfileSO equipProfile => _equipProfile;

    [SerializeField] private EquipInformation _equipInformation;
    public EquipInformation equipInformation => _equipInformation;

    List<string> equipTypes = new List<string> { "Bowman", "Warrior" };

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEquipProfileSO();
    }
    protected virtual void LoadEquipProfileSO()
    {
        if (this._equipProfile != null) return;
        foreach (string equipType in equipTypes)
        {
            string resPath = "ItemProfiles/Equipment/" + equipType + "/" + transform.parent.name;
            this._equipProfile = Resources.Load<EquipProfileSO>(resPath);
            if (_equipProfile != null) break;
        }
        Debug.LogWarning(transform.name + ": LoadEquipProfileSO", gameObject);
    }
    protected override void Start()
    {
        base.Start();
        this.SetSprite();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.SetDefaultValue();
    }

    protected virtual void SetDefaultValue()
    {
        this._equipInformation.equipProfile = _equipProfile;
        this._equipInformation.upgradeLevel = 0;
    }
    private void SetSprite()
    {
        this.equipCtrl.equipModel.SetSprite(this._equipProfile.equipSprite);
    }
    public void SetValue(EquipInformation equipInfo)
    {
        this._equipInformation.equipProfile = equipInfo.equipProfile;
        this._equipInformation.upgradeLevel = equipInfo.upgradeLevel;
    }
}
