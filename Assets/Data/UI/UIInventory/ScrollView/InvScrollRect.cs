using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
public class InvScrollRect : ScrollRect, IPointerEnterHandler, IPointerExitHandler//, IPointerDownHandler
{
    private static string mouseScrollWheelAxis = "Mouse ScrollWheel";
    private bool swallowMouseWheelScrolls = true;
    private bool isMouseOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!swallowMouseWheelScrolls && isMouseOver)
        {
            base.OnBeginDrag(eventData);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!swallowMouseWheelScrolls && isMouseOver)
        {
            base.OnDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!swallowMouseWheelScrolls && isMouseOver)
        {
            base.OnEndDrag(eventData);
        }
    }

    private void Update()
    {
        // Detect the mouse wheel and generate a scroll. This fixes the issue where Unity will prevent our ScrollRect
        // from receiving any mouse wheel messages if the mouse is over a raycast target (such as a button).
        if (isMouseOver && IsMouseWheelRolling())
        {
            Debug.Log("dc may");
            var delta = UnityEngine.Input.GetAxis(mouseScrollWheelAxis);
            Debug.Log(delta);
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            Debug.Log(pointerData);
            pointerData.scrollDelta = new Vector2(0f, delta);
            swallowMouseWheelScrolls = false;
            OnScroll(pointerData);
            swallowMouseWheelScrolls = true;
        }
    }

    public override void OnScroll(PointerEventData data)
    {
        if (IsMouseWheelRolling() && swallowMouseWheelScrolls)
        {
            // Eat the scroll so that we don't get a double scroll when the mouse is over an image
        }
        else
        {
            // Amplify the mousewheel so that it matches the scroll sensitivity.
            if (data.scrollDelta.y < -Mathf.Epsilon)
            {
                data.scrollDelta = new Vector2(0f, -scrollSensitivity);
                Debug.Log("xuong");
            }
            
            else if (data.scrollDelta.y > Mathf.Epsilon)
            {
                data.scrollDelta = new Vector2(0f, scrollSensitivity);
                Debug.Log("len");
            }

            base.OnScroll(data);
        }
    }
    

    private static bool IsMouseWheelRolling()
    {
        return UnityEngine.Input.GetAxis(mouseScrollWheelAxis) != 0;
    }
}