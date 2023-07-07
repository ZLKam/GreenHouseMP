using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3
{
    public class ComponentEvent : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        internal ComponentButtonEvent buttonEvent;

        LineManagerController lineManagerController;

        private void Start()
        {
            lineManagerController = FindObjectOfType<LineManagerController>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            lineManagerController.componentClicked = false;
            lineManagerController.componentClickedT = null;
            if (buttonEvent.transform.parent.GetComponent<ComponentWheel>().drawLine)
                return;
            buttonEvent.FollowDragPosition(eventData, transform);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("clicked");
            lineManagerController.componentClicked = true;
            if (!lineManagerController.componentClickedT)
                lineManagerController.componentClickedT = transform;

            if (lineManagerController.transform.childCount == 0)
                return;
            if (lineManagerController.transform.GetChild(lineManagerController.transform.childCount - 1).GetComponent<LineManager>().isDrawing)
            {
                lineManagerController.transform.GetChild(lineManagerController.transform.childCount - 1).GetComponent<LineManager>().SetHitT = transform;
            }
        }
    }
}