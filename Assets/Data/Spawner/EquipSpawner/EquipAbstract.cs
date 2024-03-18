using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipAbstract : SecondMonoBehaviour
{
    [Header("Equipment Abstract")]
    [SerializeField] protected EquipCtrl _equipCtrl;
    public EquipCtrl equipCtrl => _equipCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEquipCtrl();
    }

    protected virtual void LoadEquipCtrl()
    {
        if (this._equipCtrl != null) return;
        this._equipCtrl = transform.parent.GetComponent<EquipCtrl>();
        Debug.LogWarning(transform.name + ": LoadEquipCtrl", gameObject);
    }
}

