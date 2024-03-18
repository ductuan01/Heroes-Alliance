using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseAbstract : SecondMonoBehaviour
{
    [Header("Equipment Abstract")]
    [SerializeField] protected UseCtrl _useCtrl;
    public UseCtrl useCtrl => _useCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUseCtrl();
    }

    protected virtual void LoadUseCtrl()
    {
        if (this._useCtrl != null) return;
        this._useCtrl = transform.parent.GetComponent<UseCtrl>();
        Debug.LogWarning(transform.name + ": LoadUseCtrl", gameObject);
    }
}

