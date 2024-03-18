using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSlotsCtrl : SecondMonoBehaviour
{
    private static UseSlotsCtrl instance;
    public static UseSlotsCtrl Instance => instance;

    [SerializeField] protected List<UseSlot> _useSlots;
    public List<UseSlot> useSlots => _useSlots;

    protected override void Awake()
    {
        base.Awake();
        if (UseSlotsCtrl.instance != null) Debug.LogError("Only 1 UseSlotsCtrl allow to exist");
        UseSlotsCtrl.instance = this;

        this.LinkUseSlot();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUseSlots();
    }

    protected virtual void LoadUseSlots()
    {
        if (this._useSlots.Count > 0) return;
        Transform EtcSlotsCtrl = this.transform;
        foreach (Transform etcSlot in EtcSlotsCtrl)
        {
            this._useSlots.Add(etcSlot.GetComponent<UseSlot>());
        }
    }

    public virtual void LinkUseSlot()
    {
        for (int i = 0; i < PlayerInventory.Instance.UseInventory.Count; i++)
        {
            this._useSlots[i].uiUseInfo.LinkUseInfo(PlayerInventory.Instance.UseInventory[i]);
        }
    }
}
