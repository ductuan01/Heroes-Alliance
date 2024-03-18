using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbstract : SecondMonoBehaviour
{
    [Header("Player Abstract")]
    [SerializeField] private PlayerCtrl _playerCtrl;
    public PlayerCtrl PlayerCtrl => _playerCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (_playerCtrl != null) return;
        this._playerCtrl = transform.GetComponentInParent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }
}
