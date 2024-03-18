using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICommunityCtrl : MonoBehaviour
{
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    public void BtnCommunityCtrlToggle()
    {
        this._isOpen = !this._isOpen;
        if (_isOpen) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    public void HideUICommunityCtrl()
    {
        if (this._isOpen) this.BtnCommunityCtrlToggle();
    }
}
