using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropBlock : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        DraggableCodingBlock dragBlock = eventData.pointerDrag.GetComponent<DraggableCodingBlock>();
        if (transform.childCount > 0)
        {
            GetComponentInChildren<DraggableCodingBlock>().SetDropBlockOrOriginalParent(dragBlock);
        }

        dragBlock.SetDropBlockAsParent(transform);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.childCount == 0) return;

        GetComponentInChildren<DraggableCodingBlock>().SetOriginalParent();
    }
}