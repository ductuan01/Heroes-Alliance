using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsManagerCtrl : SecondMonoBehaviour
{
    private static UIButtonsManagerCtrl _instance;
    public static UIButtonsManagerCtrl Instance => _instance;

    [SerializeField] private UIButtonHideButtonsCtrl _UIButtonHideButtonsCtrl;
    [SerializeField] private UIButtonShowButtonsCtrl _UIButtonShowButtonsCtrl;
    [SerializeField] private UIManagerCtrl _UIManagerCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (UIButtonsManagerCtrl._instance != null) Debug.LogError("Only 1 UIButtonsManagerCtrl allow to exist");
        UIButtonsManagerCtrl._instance = this;
        this._UIButtonShowButtonsCtrl.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIButtonHideButtonsCtrl();
        this.LoadUIButtonShowButtonsCtrl();
        this.LoadUIManagerCtrl();
    }

    private void LoadUIButtonHideButtonsCtrl()
    {
        if (this._UIButtonHideButtonsCtrl != null) return;
        this._UIButtonHideButtonsCtrl = transform.GetComponentInChildren<UIButtonHideButtonsCtrl>();
        Debug.Log(transform.name + "LoadUIButtonHideButtonsCtrl", gameObject);
    }
    private void LoadUIButtonShowButtonsCtrl()
    {
        if (this._UIButtonShowButtonsCtrl != null) return;
        this._UIButtonShowButtonsCtrl = transform.GetComponentInChildren<UIButtonShowButtonsCtrl>();
        Debug.Log(transform.name + "LoadUIButtonShowButtonsCtrl", gameObject);
    }

    private void LoadUIManagerCtrl()
    {
        if (this._UIManagerCtrl != null) return;
        this._UIManagerCtrl = transform.GetComponentInChildren<UIManagerCtrl>();
        Debug.Log(transform.name + "LoadUIManagerCtrl", gameObject);
    }

    public void BtnHideButtonsTogle()
    {
        _UIButtonHideButtonsCtrl.gameObject.SetActive(false);
        _UIButtonShowButtonsCtrl.gameObject.SetActive(true);
        _UIManagerCtrl.HideAllUICtrl();
        _UIManagerCtrl.gameObject.SetActive(false);
    }

    public void BtnShowButtonsTogle()
    {
        _UIButtonHideButtonsCtrl.gameObject.SetActive(true);
        _UIButtonShowButtonsCtrl.gameObject.SetActive(false);
        _UIManagerCtrl.gameObject.SetActive(true);
    }
}
