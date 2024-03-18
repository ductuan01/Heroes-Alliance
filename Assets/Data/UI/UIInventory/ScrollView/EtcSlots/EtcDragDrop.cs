using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EtcDragDrop : SecondMonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] protected Transform realParent;
    [SerializeField] public Transform oldParent;

    [SerializeField] private UIEtcInfo _etcInfo;
    public UIEtcInfo etcInfo => _etcInfo;

    [SerializeField] private Image _etcImage;
    [SerializeField] protected TextMeshProUGUI _etcAmount;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadCanvasGroup();
        this.LoadUIEtcInfo();
        this.LoadEtcImage();
        this.LoadEtcAmount();
    }

    private void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Debug.LogWarning(transform.name + ": LoadCanvas", gameObject);
    }

    private void LoadCanvasGroup()
    {
        if (this._canvasGroup != null) return;
        this._canvasGroup = transform.GetComponent<CanvasGroup>();
        Debug.LogWarning(transform.name + ": LoadCanvasGroup", gameObject);
    }

    private void LoadUIEtcInfo()
    {
        if (this._etcInfo != null) return;
        this._etcInfo = transform.GetComponentInChildren<UIEtcInfo>();
        Debug.LogWarning(transform.name + ": LoadUIUseInfo", gameObject);
    }

    protected virtual void LoadEtcImage()
    {
        if (this._etcImage != null) return;
        this._etcImage = transform.GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadUseImage", gameObject);
    }
    protected virtual void LoadEtcAmount()
    {
        if (this._etcAmount != null) return;
        this._etcAmount = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadUseAmount", gameObject);
    }
    public virtual void SetUIEtc()
    {
        if (this._etcInfo.etcInformation.etcProfile == null || this._etcInfo.etcInformation.Amount == 0)
        {
            this._etcImage.sprite = null;
            this._etcAmount.SetText("");
        }
        else
        {
            Color newColor = this._etcImage.color;
            newColor.a = 1f;
            this._etcImage.color = newColor;
            this._etcImage.sprite = this._etcInfo.etcInformation.etcProfile.etcSprite;
            this._etcAmount.SetText(this._etcInfo.etcInformation.Amount.ToString());
        }
    }
    public virtual void SwapItem(Transform newParent, EtcDragDrop etcSwaped)
    {
        if (newParent == null || etcSwaped == null) return;
        this.realParent = newParent;

        etcSwaped.transform.SetParent(this.oldParent);
        transform.SetParent(this.realParent);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

        this.oldParent = transform.parent;
        this.realParent = transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if (this._etcInfo.etcInformation.etcProfile == null)
        {
            eventData.pointerDrag = null;
            return;
        }

        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;

        transform.parent = canvas.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        Vector3 mousePos = InputManager.Instance.MouseWorldPos;
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        transform.SetParent(this.realParent);
    }
}
