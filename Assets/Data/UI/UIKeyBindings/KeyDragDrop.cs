using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyDragDrop : SecondMonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] protected Transform realParent;
    [SerializeField] public Transform oldParent;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadCanvasGroup();
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

    public void SwapItem(Transform newParent, KeyDragDrop keySwaped)
    {
        if (newParent == null || keySwaped == null) return;
        this.realParent = newParent;

        keySwaped.transform.SetParent(this.oldParent);
        transform.SetParent(this.realParent);
    }

    public virtual void SetNewParent(Transform newParent)
    {
        if (newParent == null) return;
        this.realParent = newParent;
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
        
        UIKeyBindingsCtrl.Instance.SetKeyBindings();
    }
    public void SetCanvasGroupTrue()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}
