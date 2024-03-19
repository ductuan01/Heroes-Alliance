using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseDragDrop : SecondMonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] protected Transform realParent;
    [SerializeField] public Transform oldParent;

    [SerializeField] private UIUseInfo _useInfo;
    public UIUseInfo useInfo => _useInfo;

    [SerializeField] private Image _useImage;
    [SerializeField] protected TextMeshProUGUI _useAmount;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadRectTransform();
        this.LoadCanvasGroup();
        this.LoadUIUseInfo();
        this.LoadUseImage();
        this.LoadUseAmount();
    }

    private void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Debug.LogWarning(transform.name + ": LoadCanvas", gameObject);
    }

    private void LoadRectTransform()
    {
        if (this._rectTransform != null) return;
        this._rectTransform = transform.GetComponent<RectTransform>();
        Debug.LogWarning(transform.name + ": LoadRectTransform", gameObject);
    }
    private void LoadCanvasGroup()
    {
        if (this._canvasGroup != null) return;
        this._canvasGroup = transform.GetComponent<CanvasGroup>();
        Debug.LogWarning(transform.name + ": LoadCanvasGroup", gameObject);
    }

    private void LoadUIUseInfo()
    {
        if (this._useInfo != null) return;
        this._useInfo = transform.GetComponentInChildren<UIUseInfo>();
        Debug.LogWarning(transform.name + ": LoadUIUseInfo", gameObject);
    }

    protected virtual void LoadUseImage()
    {
        if (this._useImage != null) return;
        this._useImage = transform.GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadUseImage", gameObject);
    }
    protected virtual void LoadUseAmount()
    {
        if (this._useAmount != null) return;
        this._useAmount = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadUseAmount", gameObject);
    }
    public virtual void SetUIUse()
    {
        if (this._useInfo.useInformation.useProfile == null || this._useInfo.useInformation.Amount == 0)
        {
            this._useImage.sprite = null;
            this._useAmount.SetText("");
        }
        else
        {
            Color newColor = this._useImage.color;
            newColor.a = 1f;
            this._useImage.color = newColor;
            this._useImage.sprite = this._useInfo.useInformation.useProfile.useSprite;
            this._useAmount.SetText(this._useInfo.useInformation.Amount.ToString());
        }
    }
    public virtual void SwapItem(Transform newParent, UseDragDrop useSwaped)
    {
        if (newParent == null || useSwaped == null) return;
        this.realParent = newParent;

        useSwaped.transform.SetParent(this.oldParent);
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
        if (this._useInfo.useInformation.useProfile == null)
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this._useInfo.useInformation.useProfile == null)
        {
            eventData.pointerClick = null;
            return;
        }

        clicked++;
        if (clicked == 1) clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            UseDragDrop useClick = eventData.pointerClick.GetComponent<UseDragDrop>();
            if (useClick == null) return;
            if (useClick.transform.parent.parent.name == "UseSlots")
            {
                useClick._useInfo.useInformation.useItem();
            }
        }

        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    }
}
