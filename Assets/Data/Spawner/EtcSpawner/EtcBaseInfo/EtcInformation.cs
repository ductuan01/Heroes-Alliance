using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EtcInformation
{
    [Header("Real Information")]
    public EtcProfileSO etcProfile;
    public int Amount = 0;
    public int maxStack = 3;

    public void SetEtcInfo(EtcInformation etcInfo)
    {
        this.etcProfile = etcInfo.etcProfile;
        this.Amount = etcInfo.Amount;
        this.maxStack = etcInfo.maxStack;
    }

    public void SetEtcInfoNull()
    {
        this.etcProfile = null;
        this.Amount = 0;
        this.maxStack = 0;
    }
}
