using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Grey slot in the coding UI. Blocks can be placed in here
/// </summary>
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
