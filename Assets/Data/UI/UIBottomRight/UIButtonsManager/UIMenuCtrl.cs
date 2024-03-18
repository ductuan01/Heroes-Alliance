using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuCtrl : SecondMonoBehaviour
{
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    public void BtnMenuCtrlToggle()
    {
        this._isOpen = !this._isOpen;
        if (_isOpen) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    public void HideUIMenuCtrl()
    {
        if (this._isOpen) this.BtnMenuCtrlToggle();
    }
}
