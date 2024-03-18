using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEtcInfo : MonoBehaviour
{
    [SerializeField] private EtcInformation _etcInformation;
    public EtcInformation etcInformation => _etcInformation;

    public virtual void LinkEtcInfo(EtcInformation etcInformation)
    {
        this._etcInformation = etcInformation;
    }
}
