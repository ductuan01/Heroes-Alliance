using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlot : SecondMonoBehaviour, IDropHandler
{
    [SerializeField] private EquipDragDrop _equipDragDrop;
    public EquipDragDrop equipDragDrop => _equipDragDrop;

    [SerializeField] protected UIEquipInfo _uiEqiupInfo;
    public UIEquipInfo uiEquipInfo => _uiEqiupInfo;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEquipDragDrop();
        this.LoadUIEquipInfo();
    }

    protected virtual void LoadEquipDragDrop()
    {
        if (this._equipDragDrop != null) return;
        this._equipDragDrop = transform.GetComponentInChildren<EquipDragDrop>();
        Debug.LogWarning(transform.name + ": LoadEquipDragDrop", gameObject);
    }
    protected virtual void LoadUIEquipInfo()
    {
        if (this._uiEqiupInfo != null) return;
        this._uiEqiupInfo = transform.Find("EquipImage").GetComponentInChildren<UIEquipInfo>();
        Debug.LogWarning(transform.name + ": LoadUIEquipInfo", gameObject);
    }
    public void LoadDragDropAndInfoAfterSwap()
    {
        this._equipDragDrop = transform.GetComponentInChildren<EquipDragDrop>();
        this._uiEqiupInfo = transform.Find("EquipImage").GetComponentInChildren<UIEquipInfo>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        GameObject dropObj = eventData.pointerDrag;
        if (dropObj == null) return;
        EquipDragDrop equipSwap = eventData.pointerDrag.GetComponent<EquipDragDrop>();
        if (equipSwap == null) return;

        GameObject onDropObj = eventData.pointerCurrentRaycast.gameObject;
        if (onDropObj == null) return;
        EquipDragDrop equipSwaped = onDropObj.GetComponent<EquipDragDrop>();
        if (equipSwaped == null) return;

        if (equipSwap.oldParent.parent.name == "EquipSlots")
        {
            if (equipSwaped.transform.parent.parent.name == "EquipSlots")
            {
                equipSwap.SwapItem(transform, equipSwaped);
                PlayerInventory.Instance.SwapEquip(equipSwap.equipInfo.equipInformation, equipSwaped.equipInfo.equipInformation);
            }

            if (equipSwaped.transform.parent.parent.name == "PlayerEquipInvSlots")
            {
                if (equipSwap.equipInfo.equipInformation.equipProfile.equipmentType.ToString() != equipSwaped.transform.parent.name) return;
                PlayerInventory.Instance.SwapEquipData(equipSwap.equipInfo.equipInformation, equipSwaped.equipInfo.equipInformation);
                equipSwap.SetUIEquip();
                equipSwaped.SetUIEquip();

                PlayerEquipInv.Instance.LoadEquipsStats();
            }
        }

        if (equipSwap.oldParent.parent.name == "PlayerEquipInvSlots")
        {
            if (equipSwaped.transform.parent.parent.name == "EquipSlots")
            {
                PlayerInventory.Instance.SwapEquipData(equipSwap.equipInfo.equipInformation, equipSwaped.equipInfo.equipInformation);
                equipSwap.SetUIPlayerEquipInv();
                equipSwaped.SetUIEquip();

                PlayerEquipInv.Instance.LoadEquipsStats();
            }
        }

        equipSwap.oldParent.GetComponent<EquipSlot>().LoadDragDropAndInfoAfterSwap();
        this.LoadDragDropAndInfoAfterSwap();
    }
}
