using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnPointerHandler : SecondMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _BtnIcon;
    [SerializeField] private TextMeshProUGUI _BtnName;

    protected override void Awake()
    {
        base.Awake();
        this._BtnName.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBtnIcon();
        this.LoadBtnName();
    }

    private void LoadBtnIcon()
    {
        if (this._BtnIcon != null) return;
        this._BtnIcon = transform.parent.Find("ButtonImage").Find("ButtonIcon").GetComponent<Image>();
        Debug.LogWarning(transform.name + " LoadBtnIcon", gameObject);
    }

    private void LoadBtnName()
    {
        if (this._BtnName != null) return;
        this._BtnName = transform.parent.Find("ButtonImage").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + " LoadBtnName", gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == null) return;
        this._BtnIcon.gameObject.SetActive(false);
        this._BtnName.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this._BtnIcon.gameObject.SetActive(true);
        this._BtnName.gameObject.SetActive(false);
    }
}
