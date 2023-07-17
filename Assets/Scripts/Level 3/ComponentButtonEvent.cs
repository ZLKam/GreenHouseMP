using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3 
{
    public class ComponentButtonEvent : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        ComponentWheel componentWheel;

        public GameObject component;
        private Transform instantiatedComponent;
        //private static int spawnedComponentCount = 0;

        private Vector3 mousePos;

        private void Start()
        {
            componentWheel = transform.parent.GetComponent<ComponentWheel>();
            //spawnedComponentCount = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        //handles instantiting the object component to be placed in the scene
        {
            //if (spawnedComponentCount >= 7)
            //    return;
            //transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            instantiatedComponent = Instantiate(component, mousePos, Quaternion.identity, componentWheel.playArea).transform;
            instantiatedComponent.GetComponent<ComponentEvent>().buttonEvent = this;

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
            if (!component)
                return;
            FollowDragPosition(eventData, instantiatedComponent);
        }

        //handles moving the component with the mouse position
        //prevents component from going beyond set boundaries
        internal void FollowDragPosition(PointerEventData eventData, Transform component)
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
            if (instantiatedComponent)
            {
                Debug.Log(eventData.pointerCurrentRaycast.gameObject.tag);
                if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Selection"))
                {
                    Debug.Log("Component placed on selection");
                    instantiatedComponent.transform.parent = eventData.pointerCurrentRaycast.gameObject.transform;
                    instantiatedComponent.transform.localPosition = Vector3.zero;
                }
            }
        }
    }
}
