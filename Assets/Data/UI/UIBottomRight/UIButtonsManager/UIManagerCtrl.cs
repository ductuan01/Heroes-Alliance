using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerCtrl : SecondMonoBehaviour
{
    private static UIManagerCtrl _instance;
    public static UIManagerCtrl Instance => _instance;

    [SerializeField] private UIMenuCtrl _UIMenuCtrl;
    public UIMenuCtrl UIMenuCtrl => _UIMenuCtrl;

    [SerializeField] private UISettingCtrl _UISettingCtrl;
    public UISettingCtrl UISettingCtrl => _UISettingCtrl;

    [SerializeField] private UICommunityCtrl _UICommunityCtrl;
    public UICommunityCtrl UICommunityCtrl => _UICommunityCtrl;

    [SerializeField] private UICharacterCtrl _UICharacterCtrl;
    public UICharacterCtrl UICharacterCtrl => _UICharacterCtrl;

    [SerializeField] private UIEventCtrl _UIEventCtrl;
    public UIEventCtrl UIEventCtrl => _UIEventCtrl;

    [SerializeField] private UINTDShopCtrl _UINTDShopCtrl;
    public UINTDShopCtrl UINTDShopCtrl => _UINTDShopCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (UIManagerCtrl._instance != null) Debug.LogError("Only 1 UIManagerCtrl allow to exist");
        UIManagerCtrl._instance = this;
        //this._UIButtonShowButtonsCtrl.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIMenuCtrl();
        this.LoadUISettingCtrl();
        this.LoadUICommunityCtrl();
        this.LoadUICharacterCtrl();
        this.LoadUIEventCtrl();
        this.LoadUINTDShopCtrl();
        //this.LoadUIButtonShowButtonsCtrl();
        //this.LoadUIManagerCtrl();
    }

    private void LoadUIMenuCtrl()
    {
        if (this._UIMenuCtrl != null) return;
        this._UIMenuCtrl = transform.Find("UIButtonMenu").GetComponentInChildren<UIMenuCtrl>();
        Debug.Log(transform.name + "LoadUIMenuCtrl", gameObject);
        this._UIMenuCtrl.gameObject.SetActive(false);
    }
    private void LoadUISettingCtrl()
    {
        if (this._UISettingCtrl != null) return;
        this._UISettingCtrl = transform.Find("UIButtonSetting").GetComponentInChildren<UISettingCtrl>();
        Debug.Log(transform.name + "LoadUISettingCtrl", gameObject);
        this._UISettingCtrl.gameObject.SetActive(false);
    }
    private void LoadUICommunityCtrl()
    {
        if (this._UICommunityCtrl != null) return;
        this._UICommunityCtrl = transform.Find("UIButtonCommunity").GetComponentInChildren<UICommunityCtrl>();
        Debug.Log(transform.name + "LoadUICommunityCtrl", gameObject);
        this._UICommunityCtrl.gameObject.SetActive(false);
    }
    private void LoadUICharacterCtrl()
    {
        if (this._UICharacterCtrl != null) return;
        this._UICharacterCtrl = transform.Find("UIButtonCharacter").GetComponentInChildren<UICharacterCtrl>();
        Debug.Log(transform.name + "LoadUICharacterCtrl", gameObject);
        this._UICharacterCtrl.gameObject.SetActive(false);
    }
    private void LoadUIEventCtrl()
    {
        if (this._UIEventCtrl != null) return;
        this._UIEventCtrl = transform.Find("UIButtonEvent").GetComponentInChildren<UIEventCtrl>();
        Debug.Log(transform.name + "LoadUIEventCtrl", gameObject);
        this._UIEventCtrl.gameObject.SetActive(false);
    }
    private void LoadUINTDShopCtrl()
    {
        if (this._UINTDShopCtrl != null) return;
        this._UINTDShopCtrl = transform.Find("UIButtonNTDShop").GetComponentInChildren<UINTDShopCtrl>();
        Debug.Log(transform.name + "LoadUINTDShopCtrl", gameObject);
        this._UINTDShopCtrl.gameObject.SetActive(false);
    }

    public void HideAllUICtrl()
    {
        this._UIMenuCtrl.HideUIMenuCtrl();
        this._UISettingCtrl.HideUISettingCtrl();
        this._UICommunityCtrl.HideUICommunityCtrl();
        this._UICharacterCtrl.HideUIChacracterCtrl();
        this._UIEventCtrl.HideUIEventCtrl();
        // van van va van van
    }
}
