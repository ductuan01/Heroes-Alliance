using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDragDrop : SecondMonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] protected Transform realParent;
    [SerializeField] public Transform oldParent;

    [SerializeField] private UISkillInfo _skillInfo;
    public UISkillInfo skillInfo => _skillInfo;

    [SerializeField] private Image _skillImage;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadCanvasGroup();
        this.LoadUISkillInfo();
        this.LoadSkillImage();
    }

    protected override void Start()
    {
        base.Start();
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

    private void LoadUISkillInfo()
    {
        if (this._skillInfo != null) return;
        this._skillInfo = transform.GetComponentInChildren<UISkillInfo>();
        Debug.LogWarning(transform.name + ": LoadUISkillInfo", gameObject);
    }

    protected virtual void LoadSkillImage()
    {
        if (this._skillImage != null) return;
        this._skillImage = transform.GetComponent<Image>();
        this._skillImage.sprite = this.skillInfo.skillProfile.skillSprite;
        Debug.LogWarning(transform.name + ": LoadSkillImage", gameObject);
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
        foreach(Transform Skill in PlayerSkills.Instance.Skills)
        {
            if(Skill.name == this._skillInfo.skillProfile.skillCode.ToString())
            {
                if(Skill.GetComponentInChildren<SkillInfo>().CurrentSkillLevel < 1)
                {
                    eventData.pointerDrag = null;
                    return;
                }
            }
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
