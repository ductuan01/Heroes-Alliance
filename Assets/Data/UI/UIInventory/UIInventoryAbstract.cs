using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIInventoryAbstract : SecondMonoBehaviour
{
    [Header("UI Inventory Abstract")]
    [SerializeField] protected UIInventoryCtrl _uiInventoryCtrl;
    public UIInventoryCtrl uiInventoryCtrl => _uiInventoryCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIInventoryCtrl();
    }

    protected virtual void LoadUIInventoryCtrl()
    {
        if (this._uiInventoryCtrl != null) return;
        this._uiInventoryCtrl = transform.parent.GetComponent<UIInventoryCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIInventoryCtrl", gameObject);
    }
}
