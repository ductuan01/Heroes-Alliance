using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipInvSlotsCtrl : SecondMonoBehaviour
{
    private static PlayerEquipInvSlotsCtrl instance;
    public static PlayerEquipInvSlotsCtrl Instance => instance;

    [SerializeField] protected List<EquipSlot> _equipSlots;
    public List<EquipSlot> equipSlots => _equipSlots;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerEquipInvSlotsCtrl.instance != null) Debug.LogError("Only 1 PlayerEquipInvSlotsCtrl allow to exist");
        PlayerEquipInvSlotsCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEquipSlots();
    }
    private void LoadEquipSlots()
    {
        if (this._equipSlots.Count > 0) return;
        Transform EquipInvSlotsCtrl = this.transform;
        foreach (Transform equipInvSlot in EquipInvSlotsCtrl)
        {
            if(equipInvSlot.GetComponent<EquipSlot>() != null) this._equipSlots.Add(equipInvSlot.GetComponent<EquipSlot>());
        }
    }

    public void LoadEquipImage()
    {
        foreach(EquipSlot equipSlot in this._equipSlots)
        {
            EquipDragDrop equipDragDrop = equipSlot.transform.GetComponentInChildren<EquipDragDrop>();
            if (equipDragDrop == null) continue;
            equipDragDrop.SetUIPlayerEquipInv2();
        }
    }
}
