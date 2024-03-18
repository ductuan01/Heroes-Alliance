using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EtcAbstract : SecondMonoBehaviour
{
    [Header("Etc Abstract")]
    [SerializeField] protected EtcCtrl _etcCtrl;
    public EtcCtrl etcCtrl => _etcCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEtcCtrl();
    }

    protected virtual void LoadEtcCtrl()
    {
        if (this._etcCtrl != null) return;
        this._etcCtrl = transform.parent.GetComponent<EtcCtrl>();
        Debug.LogWarning(transform.name + ": LoadEtcCtrl", gameObject);
    }
}

