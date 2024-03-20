using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBaseInfo : UseAbstract
{
    [SerializeField] private UseProfileSO _useProfile;
    public UseProfileSO useProfile => _useProfile;

    [SerializeField] private UseInformation _useInformation;
    public UseInformation useInformation => _useInformation;

    List<string> useTypes = new List<string> { "Recovery", "Scroll" };

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUseProfileSO();
    }

    protected virtual void LoadUseProfileSO()
    {
        if (this._useProfile != null) return;
        foreach (string useType in useTypes)
        {
            string resPath = "ItemProfiles/Use/" + useType + "/" + transform.parent.name;
            this._useProfile = Resources.Load<UseProfileSO>(resPath);
            if (_useProfile != null) break;
        }
        Debug.LogWarning(transform.name + ": LoadUseProfileSO", gameObject);
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
        this._useInformation.useProfile = _useProfile;
        this._useInformation.Amount = _useProfile.Amount;
        this._useInformation.maxStack = _useProfile.defaultMaxStack;
    }
    private void SetSprite()
    {
        this._useCtrl.useModel.SetSprite(this._useProfile.useSprite);
    }

    public void SetValue(UseInformation useInfo)
    {
        this._useInformation.useProfile = useInfo.useProfile;
        this._useInformation.Amount = useInfo.Amount;
        Debug.Log(this._useInformation.Amount);
        this._useInformation.maxStack = useInfo.maxStack;
    }
}
