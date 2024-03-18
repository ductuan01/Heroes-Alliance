using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NTDAbstract : SecondMonoBehaviour
{
    [Header("NTD Abstract")]
    [SerializeField] protected NTDCtrl _ntdCtrl;
    public NTDCtrl ntdCtrl => _ntdCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNTDCtrl();
    }

    protected virtual void LoadNTDCtrl()
    {
        if (this._ntdCtrl != null) return;
        this._ntdCtrl = transform.parent.GetComponent<NTDCtrl>();
        Debug.LogWarning(transform.name + ": LoadNTDCtrl", gameObject);
    }
}

