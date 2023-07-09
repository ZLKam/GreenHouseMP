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
        //when dragging the gameobject resets variables
        //makes the component follow the mouse position
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
            //when line is able to draw, sets the transform of the component as the point in which the line is drawn

            if (lineManagerController.transform.childCount == 0)
                return;

            //When a line is being drawn
            if (lineManagerController.transform.GetChild(lineManagerController.transform.childCount - 1).GetComponent<LineManager>().isDrawing)
            {
                //sets the hitT variable to the transform of the new component that has been clicked
                lineManagerController.transform.GetChild(lineManagerController.transform.childCount - 1).GetComponent<LineManager>().SetHitT = transform;
            }
        }
    }
}