using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowSkillInfo : SecondMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.GetComponentInChildren<UISkillInfo>()) ShowInfo(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (UIPlayerSkillCtrl.Instance.SkillInfoCtrl.gameObject.activeSelf) UIPlayerSkillCtrl.Instance.SkillInfoCtrl.gameObject.SetActive(false);
    }


/*    protected virtual void ShowEtcInfo(PointerEventData eventData)
    {
        GameObject pointerObj = eventData.pointerEnter;
        EtcInformation etc = pointerObj.GetComponentInChildren<UIEtcInfo>()?.etcInformation;
        if (etc == null) return;
        if (etc.etcProfile == null) return;
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.SetItemName(etc);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.SetItemImage(etc);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.SetItemDescription(etc);
        UIInventoryCtrl.Instance.uiInfoCtrl.uiEtc.gameObject.SetActive(true);
    }*/

    private void ShowInfo(PointerEventData eventData)
    {
        GameObject pointerObj = eventData.pointerEnter;
        //string description = pointerObj.GetComponentInChildren<UISkillInfo>()?.skillProfile.Description;
        UISkillInfo skillInfo = pointerObj.GetComponentInChildren<UISkillInfo>();
        if (skillInfo == null) return;
        //if (etc.etcProfile == null) return;

        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.SetSkillName(skillInfo);
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.SetItemImage(skillInfo);
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.SetSkillDescription(skillInfo);
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.SetSkillInfo(skillInfo);

        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.gameObject.SetActive(true);
    }
}
