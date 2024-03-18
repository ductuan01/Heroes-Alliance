using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcSlotsCtrl : SecondMonoBehaviour
{
    private static EtcSlotsCtrl instance;
    public static EtcSlotsCtrl Instance => instance;

    [SerializeField] protected List<EtcSlot> _etcSlots;
    public List<EtcSlot> etcSlots => _etcSlots;

    protected override void Awake()
    {
        base.Awake();
        if (EtcSlotsCtrl.instance != null) Debug.LogError("Only 1 EtcSlotsCtrl allow to exist");
        EtcSlotsCtrl.instance = this;

        this.LinkEtcSlot();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEtcSlots();
    }

    protected virtual void LoadEtcSlots()
    {
        if (this._etcSlots.Count > 0) return;
        Transform EtcSlotsCtrl = this.transform;
        foreach (Transform etcSlot in EtcSlotsCtrl)
        {
            this._etcSlots.Add(etcSlot.GetComponent<EtcSlot>());
        }
    }

    public virtual void LinkEtcSlot()
    {
        for (int i = 0; i < PlayerInventory.Instance.EtcInventory.Count; i++)
        {
            this._etcSlots[i].uiEtcInfo.LinkEtcInfo(PlayerInventory.Instance.EtcInventory[i]);
        }
    }
}
