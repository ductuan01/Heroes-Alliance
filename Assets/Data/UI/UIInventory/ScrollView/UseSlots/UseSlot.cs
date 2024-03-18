using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseSlot : SecondMonoBehaviour, IDropHandler
{
    [SerializeField] private UseDragDrop _useDragDrop;
    public UseDragDrop useDragDrop => _useDragDrop;

    [SerializeField] private UIUseInfo _uiUseInfo;
    public UIUseInfo uiUseInfo => _uiUseInfo;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUseDragDrop();
        this.LoadUIUseInfor();
    }

    protected virtual void LoadUseDragDrop()
    {
        if (this._useDragDrop != null) return;
        this._useDragDrop = transform.GetComponentInChildren<UseDragDrop>();
        Debug.LogWarning(transform.name + ": LoadUseDragDrop", gameObject);
    }

    protected virtual void LoadUIUseInfor()
    {
        if (this._uiUseInfo != null) return;
        this._uiUseInfo = transform.Find("UseImage").GetComponentInChildren<UIUseInfo>();
        Debug.LogWarning(transform.name + ": LoadUIUseInfor", gameObject);
    }

    public virtual void LoadDragDropAndInfoAfterSwap()
    {
        this._useDragDrop = transform.GetComponentInChildren<UseDragDrop>();
        this._uiUseInfo = transform.Find("UseImage").GetComponentInChildren<UIUseInfo>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        GameObject dropObj = eventData.pointerDrag;
        if (dropObj == null) return;
        UseDragDrop useSwap = eventData.pointerDrag.GetComponent<UseDragDrop>();
        if (useSwap == null) return;

        GameObject onDropObj = eventData.pointerCurrentRaycast.gameObject;
        if (onDropObj == null) return;
        UseDragDrop useSwaped = onDropObj.GetComponent<UseDragDrop>();
        if (useSwaped == null) return;

        if (useSwaped.transform.parent.parent.name == "UseSlots")
        {
            useSwap.SwapItem(this.transform, useSwaped);
            PlayerInventory.Instance.SwapUse(useSwap.useInfo.useInformation, useSwaped.useInfo.useInformation);
        }

        useSwap.oldParent.GetComponent<UseSlot>().LoadDragDropAndInfoAfterSwap();
        this.LoadDragDropAndInfoAfterSwap();
    }
}
