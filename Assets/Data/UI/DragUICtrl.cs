using UnityEngine;
using UnityEngine.EventSystems;

public class DragUICtrl : SecondMonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Canvas _canvas;

    private bool isDragging = false;
    private Vector2 initialPosition;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRectTransform();
        this.LoadCanvas();
    }

    private void LoadRectTransform()
    {
        if (this._rectTransform != null) return;
        this._rectTransform = transform.parent.GetComponent<RectTransform>();
        Debug.LogWarning(transform.name + " LoadRectTransform", gameObject);
    }
    
    private void LoadCanvas()
    {
        if (this._canvas != null) return;
        this._canvas = transform.parent.GetComponentInParent<Canvas>();
        Debug.LogWarning(transform.name + " LoadCanvas", gameObject);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
            eventData.position, _canvas.worldCamera, out initialPosition);

        _rectTransform.transform.parent = _canvas.transform.Find("ForArrangeFirst").transform;
        _rectTransform.transform.parent = _canvas.transform;

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                eventData.position, _canvas.worldCamera, out Vector2 localPoint);

            Vector3 offset = localPoint - initialPosition;

            _rectTransform.localPosition += offset;

            initialPosition = localPoint;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}