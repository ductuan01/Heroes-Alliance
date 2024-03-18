using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTDInfo : NTDAbstract
{
    [SerializeField] protected int _ntdAmount;
    public int ntdAmount => _ntdAmount;

    protected override void Start()
    {
        base.Start();
        this.SetSprite();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this._ntdAmount = Random.Range(this._ntdCtrl.ntdProfile.minNTD, this._ntdCtrl.ntdProfile.maxNTD);
    }
    
    private void SetSprite()
    {
        this.ntdCtrl.ndtModel.SetSprite(this.ntdCtrl.ntdProfile.coinSprite);
    }
/*
    protected override void ResetValue()
    {
        base.ResetValue();
        this._ntdAmount = Random.Range(this._ntdCtrl.NTDProfile.minNTD, this._ntdCtrl.NTDProfile.maxNTD);
        Debug.Log(_ntdAmount);
    }

    public virtual void SetNTDAmount(int amount)
    {
        this._ntdAmount = amount;
    }*/
}
