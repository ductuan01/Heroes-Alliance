using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShowUIInfo : SecondMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == null) return;
        if (eventData.pointerEnter.GetComponentInChildren<UIEquipInfo>()) ShowEquipInfo(eventData);
        if (eventData.pointerEnter.GetComponentInChildren<UIUseInfo>()) ShowUseInfo(eventData);
        if (eventData.pointerEnter.GetComponentInChildren<UIEtcInfo>()) ShowEtcInfo(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (UIInventoryCtrl.Instance.uiInfoCtrl.uiEquip.gameObject.activeSelf) UIInventoryCtrl.Instance.uiInfoCtrl.uiEquip.gameObject.SetActive(false);
        if (UIInventoryCtrl.Instance.uiInfoCtrl.uiUse.gameObject.activeSelf) UIInventoryCtrl.Instance.uiInfoCtrl.uiUse.gameObject.SetActive(false);
        if (UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.gameObject.activeSelf) UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.gameObject.SetActive(false);
    }

    protected virtual void ShowEquipInfo(PointerEventData eventData)
    {
        GameObject pointerObj = eventData.pointerEnter;
        EquipInformation equipment = pointerObj.GetComponentInChildren<UIEquipInfo>()?.equipInformation;
        if (equipment == null) return;
        if (equipment.equipProfile == null) return;
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEquip.SetItemName(equipment);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEquip.SetItemImage(equipment);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEquip.SetItemInfo(equipment);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEquip.gameObject.SetActive(true);
        Vector3 mousePos = InputManager.Instance.MouseWorldPos;
        UIInventoryCtrl.Instance.uiInfoCtrl.transform.position = mousePos;
    }

    protected virtual void ShowUseInfo(PointerEventData eventData)
    {
        GameObject pointerObj = eventData.pointerEnter;
        UseInformation use = pointerObj.GetComponentInChildren<UIUseInfo>()?.useInformation;
        if (use == null) return;
        if (use.useProfile == null) return;
        UIInventoryCtrl.Instance.uiInfoCtrl.uiUse.SetItemName(use);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiUse.SetItemImage(use);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiUse.SetItemDescription(use);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiUse.gameObject.SetActive(true);
    }

    protected virtual void ShowEtcInfo(PointerEventData eventData)
    {
        GameObject pointerObj = eventData.pointerEnter;
        EtcInformation etc = pointerObj.GetComponentInChildren<UIEtcInfo>()?.etcInformation;
        if (etc == null) return;
        if (etc.etcProfile == null) return;
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.SetItemName(etc);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.SetItemImage(etc);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.SetItemDescription(etc);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.gameObject.SetActive(true);
    }
}