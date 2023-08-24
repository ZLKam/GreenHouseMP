using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3 
{
    public class ComponentButtonEvent : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        internal ComponentWheel componentWheel;

        public GameObject component;
        [SerializeField]
        private Transform instantiatedComponent;
        //private static int spawnedComponentCount = 0;

        private Vector3 mousePos;

        private void Start()
        {
            componentWheel = transform.parent.GetComponent<ComponentWheel>();
            //spawnedComponentCount = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (componentWheel.DrawLine)
                return;
#if UNITY_ANDROID
            if (Input.touchCount > 1)
                return;
#endif
            //if (spawnedComponentCount >= 7)
            //    return;
            //transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);

            // When the user finger / mouse pointer is down, instantiate the component and set the properties of the component
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            instantiatedComponent = Instantiate(component, mousePos, Quaternion.identity, componentWheel.playArea).transform;
            instantiatedComponent.GetComponent<ComponentEvent>().buttonEvent = this;
            instantiatedComponent.GetComponent<ComponentEvent>().holding = true;

            //component = Instantiate(transform.GetChild(transform.childCount - 1), mousePos, Quaternion.identity, componentWheel.playArea);
            //component.GetComponent<ComponentEvent>().buttonEvent = this;

            //transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            //spawnedComponentCount++;

            //if (componentWheel.drawLine && spawnedComponentCount > 1)
            //{
            //    FindObjectOfType<LineManagerController>().GetComponent<LineManagerController>().linesToDraw++;
            //}
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!instantiatedComponent || componentWheel.DrawLine)
                return;
#if UNITY_ANDROID
            if (Input.touchCount > 1)
            {
                Destroy(instantiatedComponent.gameObject);
                return;
            }
#endif
            FollowDragPosition(instantiatedComponent);
        }

        /// <summary>
        /// The component will follow the input position
        /// </summary>
        /// <param name="component">The component that will be dragged</param>
        internal void FollowDragPosition(Transform component)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            component.transform.position = mousePos;
            //if (componentWheel.IsWithinX(mousePos) && componentWheel.IsWithinY(mousePos))
            //{
            //    component.transform.position = mousePos;
            //    return;
            //}
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (componentWheel.DrawLine)
                return;
#if UNITY_ANDROID
            if (Input.touchCount > 1)
            {
                Destroy(instantiatedComponent.gameObject);
                return;
            }
#endif
            /// If there is component instantiated
            /// Check the pointer position if it hits anything
            /// When it hits a placeholder, place the component on the placeholder
            /// When it hits another component, replace the component with the new one
            /// Else, when it hits other things, destroy the component
            if (instantiatedComponent)
            {
                instantiatedComponent.GetComponent<ComponentEvent>().holding = false;
                if (!eventData.pointerCurrentRaycast.gameObject)
                {
                    Destroy(instantiatedComponent.gameObject);
                    instantiatedComponent = null;
                    return;
                }
                if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Selection"))
                {
                    Transform placeholder = eventData.pointerCurrentRaycast.gameObject.transform;
                    placeholder.GetComponent<SpriteRenderer>().enabled = false;
                    instantiatedComponent.transform.parent = placeholder;
                    instantiatedComponent.transform.localPosition = Vector3.zero;
                    // This is because the collider of cooling tower is different from the other components
                    if (instantiatedComponent.GetComponent<ComponentEvent>().componentName == "Cooling Tower")
                    {
                        instantiatedComponent.GetComponent<PolygonCollider2D>().enabled = true;
                    }
                    else
                    {
                        instantiatedComponent.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    instantiatedComponent.GetComponent<ComponentEvent>().placeholder = placeholder;
                    instantiatedComponent.GetComponent<ComponentEvent>().CheckDirection(instantiatedComponent.gameObject);
                }
                else if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Component"))
                {
                    Transform placeholder = eventData.pointerCurrentRaycast.gameObject.transform.parent;
                    Destroy(eventData.pointerCurrentRaycast.gameObject);
                    placeholder.GetComponent<SpriteRenderer>().enabled = false;
                    instantiatedComponent.transform.parent = placeholder;
                    instantiatedComponent.transform.localPosition = Vector3.zero;
                    if (instantiatedComponent.GetComponent<ComponentEvent>().componentName == "Cooling Tower")
                    {
                        instantiatedComponent.GetComponent<PolygonCollider2D>().enabled = true;
                    }
                    else
                    {
                        instantiatedComponent.GetComponent<BoxCollider2D>().enabled = true;
                    }
                    instantiatedComponent.GetComponent<ComponentEvent>().placeholder = placeholder;
                    instantiatedComponent.GetComponent<ComponentEvent>().CheckDirection(instantiatedComponent.gameObject);
                }
                else
                {
                    Destroy(instantiatedComponent.gameObject);
                    instantiatedComponent = null;
                }
            }
        }
    }
}
