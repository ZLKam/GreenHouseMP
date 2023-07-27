using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Level3
{
    public class ComponentEvent : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerCaptureEvent
    {
        internal ComponentButtonEvent buttonEvent;

        public int specialID;

        [SerializeField]
        internal Transform placeholder;

        public Transform correctTarget;
        public Transform correctTarget2 = null;
        public Color correctColor;
        public Color correctColor2;

        internal bool holding;
        private Sprite defaultPlaceholder;
        private Sprite highlightPlaceholder;
        private SpriteRenderer highlighted;
        private bool hitPlaceholder = false;

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

        private void Start()
        {
            highlightPlaceholder = Resources.Load<Sprite>("Game UI/BorderYellow");
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!buttonEvent.GetComponentInParent<ComponentWheel>())
                return;
            GetComponent<BoxCollider2D>().enabled = false;
            buttonEvent.FollowDragPosition(transform);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!buttonEvent.GetComponentInParent<ComponentWheel>())
                return;
            holding = true;
        }

        private void Update()
        {
            if (!holding)
                return;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
            if (hits.Length > 0)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.CompareTag("Selection"))
                    {
                        hitPlaceholder = true;
                        if (hit.transform.GetComponent<SpriteRenderer>().sprite != highlightPlaceholder)
                        {
                            defaultPlaceholder = hit.transform.GetComponent<SpriteRenderer>().sprite;
                            highlighted = hit.transform.GetComponent<SpriteRenderer>();
                            highlighted.sprite = highlightPlaceholder;
                        }
                        hitPlaceholder = false;
                        return;
                    }
                }
            }
            if (!hitPlaceholder)
            {
                if (defaultPlaceholder)
                    highlighted.sprite = defaultPlaceholder;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!buttonEvent.GetComponentInParent<ComponentWheel>())
                return;
            CheckPlaceholder(eventData);
            GetComponent<BoxCollider2D>().enabled = true;
            holding = false;
        }

        private void CheckPlaceholder(PointerEventData eventData)
        {
            GameObject currentRaycast = eventData.pointerCurrentRaycast.gameObject;
            if (!currentRaycast)
            {
                placeholder.GetComponent<SpriteRenderer>().enabled = true;
                Destroy(gameObject);
                return;
            }
            if (currentRaycast.CompareTag("Selection"))
            {
                if (currentRaycast.transform == placeholder)
                {
                    // this placeholder
                    transform.localPosition = Vector3.zero;
                }
                else
                {
                    if (currentRaycast.transform != placeholder)
                    {
                        // other placeholder
                        if (currentRaycast.transform.childCount == 0)
                        {
                            // not occupied
                            placeholder.GetComponent<SpriteRenderer>().enabled = true;
                            placeholder = currentRaycast.transform;
                            placeholder.GetComponent<SpriteRenderer>().enabled = false;
                            transform.parent = currentRaycast.transform;
                            transform.localPosition = Vector3.zero;
                        }
                    }
                }
            }
            else
            {
                if (currentRaycast.CompareTag("Component"))
                {
                    // occupied, so swap components
                    Transform otherComponent = currentRaycast.transform;
                    Transform otherPlaceholder = currentRaycast.transform.parent;
                    otherComponent.parent = placeholder;
                    otherComponent.localPosition = Vector3.zero;
                    placeholder = otherPlaceholder;
                    transform.parent = placeholder;
                    transform.localPosition = Vector3.zero;
                    return;
                }
                placeholder.GetComponent<SpriteRenderer>().enabled = true;
                Destroy(gameObject);
            }
        }
    }
}