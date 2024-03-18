using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfoCtrl : SecondMonoBehaviour
{
    private static UIInfoCtrl instance;
    public static UIInfoCtrl Instance => instance;

    public UIEquipInfoCtrl uiEquip;
    public UIUseInfoCtrl uiUse;
    public UIEtcInfoCtrl uiEtc;

    protected override void Awake()
    {
        base.Awake();
        if (UIInfoCtrl.instance != null) Debug.LogError("Only 1 UIInventoryCtrl allow to exist");
        UIInfoCtrl.instance = this;

        uiEquip.gameObject.SetActive(false);
        uiUse.gameObject.SetActive(false);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIEquipInfoCtrl();
        this.LoadUIUseInfoCtrl();
        this.LoadUIEtcInfoCtrl();
    }

    protected virtual void LoadUIEquipInfoCtrl()
    {
        if (this.uiEquip != null) return;
        this.uiEquip = transform.GetComponentInChildren<UIEquipInfoCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIEquipInfoCtrl", gameObject);
    }

    protected virtual void LoadUIUseInfoCtrl()
    {
        if (this.uiUse != null) return;
        this.uiUse = transform.GetComponentInChildren<UIUseInfoCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIUseInfoCtrl", gameObject);
    }

    protected virtual void LoadUIEtcInfoCtrl()
    {
        if (this.uiEtc != null) return;
        this.uiEtc = transform.GetComponentInChildren<UIEtcInfoCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIEtcInfoCtrl", gameObject);
    }
}
