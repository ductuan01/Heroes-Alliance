using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipDragDrop : SecondMonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] protected Transform realParent;
    [SerializeField] public Transform oldParent;

    [SerializeField] private UIEquipInfo _equipInfo;
    public UIEquipInfo equipInfo => _equipInfo;

    [SerializeField] private Image _EquipImage;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadCanvasGroup();
        this.LoadUIEquipInfo();
        this.LoadUseImage();
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

    private void LoadUIEquipInfo()
    {
        if (this._equipInfo != null) return;
        this._equipInfo = transform.GetComponentInChildren<UIEquipInfo>();
        Debug.LogWarning(transform.name + ": LoadUIEquipInfo", gameObject);
    }

    protected virtual void LoadUseImage()
    {
        if (this._EquipImage != null) return;
        this._EquipImage = transform.GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadUseImage", gameObject);
    }

    public virtual void SetUIEquip()
    {
        if (this._equipInfo.equipInformation.equipProfile == null) this._EquipImage.sprite = null;
        else
        {
            Color newColor = this._EquipImage.color;
            newColor.a = 1f;
            this._EquipImage.color = newColor;
            this._EquipImage.sprite = this._equipInfo.equipInformation.equipProfile.equipSprite;
        }
    }

    public virtual void SetUIPlayerEquipInv()
    {
        Color newColor = this._EquipImage.color;
        newColor.a = 0f;
        this._EquipImage.color = newColor;
        this._EquipImage.sprite = null;
    }

    public virtual void SwapItem(Transform newParent, EquipDragDrop equipSwaped)
    {
        if (newParent == null || equipSwaped == null) return;
        this.realParent = newParent;

        equipSwaped.transform.SetParent(this.oldParent);
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
        if (this._equipInfo.equipInformation.equipProfile == null)
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
        if (this._equipInfo.equipInformation.equipProfile == null)
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
            EquipDragDrop equipClick = eventData.pointerClick.GetComponent<EquipDragDrop>();
            Debug.Log(equipClick.transform.parent.parent.name);
            if (equipClick.transform.parent.parent.name == "EquipSlots")
            {
                if (equipClick != null)
                {
                    foreach (EquipSlot equipSlot in PlayerEquipInvSlotsCtrl.Instance.equipSlots)
                    {
                        if (equipSlot.name == equipClick.equipInfo.equipInformation.equipProfile.equipmentType.ToString())
                        {
                            EquipDragDrop equipRemoved = equipSlot.GetComponentInChildren<EquipDragDrop>();

                            PlayerInventory.Instance.SwapEquipData(equipClick.equipInfo.equipInformation, equipRemoved.equipInfo.equipInformation);

                            equipClick.SetUIEquip();
                            equipRemoved.SetUIEquip();

                            PlayerEquipInv.Instance.LoadEquipsStats();
                            return;
                        }
                    }
                }
            }
            if (equipClick.transform.parent.parent.name == "PlayerEquipInvSlots")
            {
                PlayerInventory.Instance.AddEquipToInv(equipClick.equipInfo.equipInformation);
                equipClick.equipInfo.SetEquipInforNull();
                equipClick.SetUIPlayerEquipInv();

                PlayerEquipInv.Instance.LoadEquipsStats();
            }
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
    }
}
