using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterCtrl : SecondMonoBehaviour
{
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    public void BtnCharacterCtrlToggle()
    {
        this._isOpen = !this._isOpen;
        if (_isOpen) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    public void HideUIChacracterCtrl()
    {
        if (this._isOpen) this.BtnCharacterCtrlToggle();
    }
}
