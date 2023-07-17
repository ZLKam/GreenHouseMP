using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3
{
    public class ComponentEvent : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        internal ComponentButtonEvent buttonEvent;

        #region Unused
        //internal ComponentButtonEvent buttonEvent;

        ////LineManagerController lineManagerController;

        //public Transform correctTransform;
        //[SerializeField]
        //private Transform correctTransform2;

        //private bool correctConnection = false;

        //[SerializeField]
        //protected int specialID;

        //private void Start()
        //{
        //    //lineManagerController = FindObjectOfType<LineManagerController>();
        //}

        //public void OnDrag(PointerEventData eventData)
        ////when dragging the gameobject resets variables
        ////makes the component follow the mouse position
        //{
        //    //lineManagerController.componentClicked = false;
        //    //lineManagerController.componentClickedT = null;
        //    if (buttonEvent.transform.parent.GetComponent<ComponentWheel>().drawLine)
        //        return;
        //    buttonEvent.FollowDragPosition(eventData, transform);
        //}

        //public void OnPointerDown(PointerEventData eventData)
        //{
        //    //lineManagerController.componentClicked = true;

        //    //if (!lineManagerController.componentClickedT)
        //    //    lineManagerController.componentClickedT = transform.GetChild(0);
        //    ////when line is able to draw, sets the transform of the component as the point in which the line is drawn

        //    //if (lineManagerController.transform.childCount == 0)
        //    //    return;

        //    //When a line is being drawn
        //    //if (lineManagerController.transform.GetChild(lineManagerController.transform.childCount - 1).GetComponent<LineManager>().isDrawing)
        //    //{
        //    //    //sets the hitT variable to the transform of the new component that has been clicked
        //    //    lineManagerController.transform.GetChild(lineManagerController.transform.childCount - 1).GetComponent<LineManager>().SetHitT = transform.GetChild(1);
        //    //    Transform fromT = lineManagerController.componentClickedT.transform.parent;
        //    //    if (fromT.GetComponent<ComponentEvent>().IsCloneOf(correctTransform) || (correctTransform2 ? fromT.GetComponent<ComponentEvent>().IsCloneOf(correctTransform2) : false))
        //    //    {
        //    //        correctConnection = true;
        //    //    }
        //    //}
        //}

        //public bool IsCloneOf(Transform original)
        //{
        //    return GetComponent<ComponentEvent>().specialID == original.GetComponent<ComponentEvent>().specialID;
        //}

        //public bool CorrectConnection
        //{
        //    get { return correctConnection; }
        //    set { correctConnection = value; }
        //}

        //public void ResetAllowDraw()
        //{
        //    GetComponentsInChildren<LineLimit>()[0].AllowDrawLine = true;
        //    GetComponentsInChildren<LineLimit>()[1].AllowDrawLine = true;
        //}

        //public bool AllowDrawLine()
        //{
        //    if (GetComponentsInChildren<LineLimit>()[0].AllowDrawLine && GetComponentsInChildren<LineLimit>()[1].AllowDrawLine)
        //        return true;
        //    return false;
        //}
        #endregion
        public void OnDrag(PointerEventData eventData)
        {
            buttonEvent.FollowDragPosition(eventData, transform);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}