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

    private void ShowInfo(PointerEventData eventData)
    {
        GameObject pointerObj = eventData.pointerEnter;
        UISkillInfo skillInfo = pointerObj.GetComponentInChildren<UISkillInfo>();
        if (skillInfo == null) return;
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.SetSkillName(skillInfo);
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.SetItemImage(skillInfo);
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.SetSkillDescription(skillInfo);
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.SetSkillInfo(skillInfo);
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.gameObject.SetActive(true);
        Vector3 mousePos = InputManager.Instance.MouseWorldPos;
        UIPlayerSkillCtrl.Instance.SkillInfoCtrl.transform.position = mousePos;
    }

}
