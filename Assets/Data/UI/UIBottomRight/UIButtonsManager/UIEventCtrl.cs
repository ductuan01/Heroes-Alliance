using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventCtrl : MonoBehaviour
{
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    public void BtnEventCtrlToggle()
    {
        this._isOpen = !this._isOpen;
        if (_isOpen) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    public void HideUIEventCtrl()
    {
        if (this._isOpen) this.BtnEventCtrlToggle();
    }
}
