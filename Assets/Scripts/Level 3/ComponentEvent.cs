using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3
{
    public class ComponentEvent : MonoBehaviour, IDragHandler
    {
        internal ComponentButtonEvent buttonEvent;

        public void OnDrag(PointerEventData eventData)
        {
            buttonEvent.FollowDragPosition(eventData, transform);
        }
    }
}