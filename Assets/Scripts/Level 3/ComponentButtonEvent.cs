using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3 {
    public class ComponentButtonEvent : MonoBehaviour, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerDrag.name + " is being dragged. Dragging to " + eventData.position);
        }
    }
}
