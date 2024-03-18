using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotsCtrl : SecondMonoBehaviour
{
    private static EquipSlotsCtrl _instance;
    public static EquipSlotsCtrl Instance => _instance;

    [SerializeField] protected List<EquipSlot> _equipSlots;
    public List<EquipSlot> equipSlots => _equipSlots;

    protected override void Awake()
    {
        base.Awake();
        if (EquipSlotsCtrl._instance != null) Debug.LogError("Only 1 EquipSlotsCtrl allow to exist");
        EquipSlotsCtrl._instance = this;

        this.LinkEquipSlots();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEquipSlots();
    }

    protected virtual void LoadEquipSlots()
    {
        if (this._equipSlots.Count > 0) return;
        Transform EquipSlotsCtrl = this.transform;
        foreach (Transform etcSlot in EquipSlotsCtrl)
        {
            this._equipSlots.Add(etcSlot.GetComponent<EquipSlot>());
        }
    }

    public virtual void LinkEquipSlots()
    {
        for (int i = 0; i < PlayerInventory.Instance.EquipInventory.Count; i++)
        {
            this._equipSlots[i].uiEquipInfo.LinkEquipInfo(PlayerInventory.Instance.EquipInventory[i]);
        }
    }
}
