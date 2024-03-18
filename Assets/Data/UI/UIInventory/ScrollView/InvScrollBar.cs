using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvScrollBar : Scrollbar, IDragHandler
{
    public override void OnDrag(PointerEventData eventData)
    {
        if (!interactable || handleRect == null)
            return;

        float axisValue = direction == Scrollbar.Direction.BottomToTop ? eventData.delta.y : eventData.delta.x;
        float normalizedValue = 0;

        if (axisValue < 0)
        {
            normalizedValue = value - (1f / 6f);
        }
        if (axisValue > 0)
        {
            normalizedValue = value + (1f / 6f);
        }
        value = Mathf.Clamp01(normalizedValue);
    }
}
