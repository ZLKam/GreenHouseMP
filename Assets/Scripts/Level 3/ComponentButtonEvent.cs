using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3 
{
    public class ComponentButtonEvent : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        ComponentWheel componentWheel;

        private Transform component;
        private static int spawnedComponentCount = 0;

        private Vector3 mousePos;

        private void Start()
        {
            componentWheel = transform.parent.GetComponent<ComponentWheel>();
            spawnedComponentCount = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        //handles instantiting the object component to be placed in the scene
        {
            if (spawnedComponentCount >= 7)
                return;
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            component = Instantiate(transform.GetChild(transform.childCount - 1), mousePos, Quaternion.identity, componentWheel.playArea);
            component.GetComponent<ComponentEvent>().buttonEvent = this;

            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
            spawnedComponentCount++;

            if (componentWheel.drawLine && spawnedComponentCount > 1)
            {
                FindObjectOfType<LineManagerController>().GetComponent<LineManagerController>().linesToDraw++;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!component)
                return;
            FollowDragPosition(eventData, component);
        }

        //handles moving the component with the mouse position
        //prevents component from going beyond set boundaries
        internal void FollowDragPosition(PointerEventData eventData, Transform component)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            if (componentWheel.IsWithinX(mousePos) && componentWheel.IsWithinY(mousePos))
            {
                component.transform.position = mousePos;
                return;
            }
        }
    }
}
