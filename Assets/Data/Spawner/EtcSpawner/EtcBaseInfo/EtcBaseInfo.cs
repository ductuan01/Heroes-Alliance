using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcBaseInfo : EtcAbstract
{
    [SerializeField] private EtcProfileSO _etcProfile;
    public EtcProfileSO etcProfile => _etcProfile;

    [SerializeField] private EtcInformation _etcInformation;
    public EtcInformation etcInformation => _etcInformation;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEtcProfileSO();
    }

    protected virtual void LoadEtcProfileSO()
    {
        if (this._etcProfile != null) return;
        string resPath = "ItemProfiles/Etc/" + transform.parent.name;
        this._etcProfile = Resources.Load<EtcProfileSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadEtcProfileSO", gameObject);
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
        this._etcInformation.etcProfile = etcProfile;
        this._etcInformation.Amount = etcProfile.Amount;
        this._etcInformation.maxStack = etcProfile.defaultMaxStack;
    }

    private void SetSprite()
    {
        this._etcCtrl.etcModel.SetSprite(this._etcProfile.etcSprite);
    }
    public void SetValue(EtcInformation etcInformation)
    {
        this._etcInformation.etcProfile = etcInformation.etcProfile;
        this._etcInformation.Amount = etcInformation.Amount;
        this._etcInformation.maxStack = etcInformation.maxStack;
    }
}
