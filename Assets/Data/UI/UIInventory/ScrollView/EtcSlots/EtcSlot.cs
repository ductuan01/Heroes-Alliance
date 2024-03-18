using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EtcSlot : SecondMonoBehaviour, IDropHandler
{
    [SerializeField] private EtcDragDrop _etcDragDrop;
    public EtcDragDrop etcDragDrop => _etcDragDrop;

    [SerializeField] protected UIEtcInfo _uiEtcInfo;
    public UIEtcInfo uiEtcInfo => _uiEtcInfo;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEtcDragDrop();
        this.LoadUIEtcInfor();
    }

    protected virtual void LoadEtcDragDrop()
    {
        if (this._etcDragDrop != null) return;
        this._etcDragDrop = transform.GetComponentInChildren<EtcDragDrop>();
        Debug.LogWarning(transform.name + ": LoadEtcDragDrop", gameObject);
    }

    protected virtual void LoadUIEtcInfor()
    {
        if (this._uiEtcInfo != null) return;
        this._uiEtcInfo = transform.Find("EtcImage").GetComponentInChildren<UIEtcInfo>();
        Debug.LogWarning(transform.name + ": LoadUIEtcInfor", gameObject);
    }

    public void LoadDragDropAndInfoAfterSwap()
    {
        this._etcDragDrop = transform.GetComponentInChildren<EtcDragDrop>();
        this._uiEtcInfo = transform.Find("EtcImage").GetComponentInChildren<UIEtcInfo>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        GameObject dropObj = eventData.pointerDrag;
        if (dropObj == null) return;
        EtcDragDrop etcSwap = eventData.pointerDrag.GetComponent<EtcDragDrop>();
        if (etcSwap == null) return;

        GameObject onDropObj = eventData.pointerCurrentRaycast.gameObject;
        if (onDropObj == null) return;
        EtcDragDrop etcSwaped = eventData.pointerCurrentRaycast.gameObject.GetComponent<EtcDragDrop>();
        if (etcSwaped == null) return;

        if (onDropObj.transform.parent.parent.name == "EtcSlots")
        {
            etcSwap.SwapItem(this.transform, etcSwaped);
            PlayerInventory.Instance.SwapEtc(etcSwap.etcInfo.etcInformation, etcSwaped.etcInfo.etcInformation);
        }

        etcSwap.oldParent.GetComponent<EtcSlot>().LoadDragDropAndInfoAfterSwap();
        this.LoadDragDropAndInfoAfterSwap();
    }
}
