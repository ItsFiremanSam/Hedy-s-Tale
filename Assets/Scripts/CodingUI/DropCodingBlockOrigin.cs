using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.CodingUI
{
    public class DropCodingBlockOrigin : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            eventData.pointerDrag.GetComponent<DraggableCodingBlock>().SetOriginalParent();
        }
    }
}