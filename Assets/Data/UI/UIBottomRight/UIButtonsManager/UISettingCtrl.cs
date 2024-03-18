using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingCtrl : SecondMonoBehaviour
{
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    public void BtnSettingCtrlToggle()
    {
        this._isOpen = !this._isOpen;
        if (_isOpen) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    public void HideUISettingCtrl()
    {
        if (this._isOpen) this.BtnSettingCtrlToggle();
    }
}
