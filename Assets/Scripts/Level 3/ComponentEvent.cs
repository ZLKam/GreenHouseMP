using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3
{
    public class ComponentEvent : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        internal ComponentButtonEvent buttonEvent;

        public void OnDrag(PointerEventData eventData)
        {
            LineManagerController.componentClicked = false;
            if (buttonEvent.transform.parent.GetComponent<ComponentWheel>().drawLine)
                return;
            buttonEvent.FollowDragPosition(eventData, transform);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("clicked");
            LineManagerController.componentClicked = true;
        }
    }
}