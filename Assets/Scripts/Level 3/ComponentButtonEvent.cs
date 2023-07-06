using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level3 {
    public class ComponentButtonEvent : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        ComponentWheel componentWheel;

        private Transform component;

        private void Start()
        {
            componentWheel = transform.parent.GetComponent<ComponentWheel>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            component = Instantiate(transform.GetChild(transform.childCount - 1), eventData.position, Quaternion.identity, componentWheel.playArea);
            component.GetComponent<ComponentEvent>().buttonEvent = this;
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            FollowDragPosition(eventData, component);
        }

        

        internal void FollowDragPosition(PointerEventData eventData, Transform component)
        {
            if (componentWheel.IsWithinX(eventData.position) && componentWheel.IsWithinY(eventData.position))
            {
                component.GetComponent<RectTransform>().position = eventData.position;
                return;
            }
        }
    }
}
