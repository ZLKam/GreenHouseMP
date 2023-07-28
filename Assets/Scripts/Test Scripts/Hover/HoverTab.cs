using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTab : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public HoverGroup hoverGroup;

    public Vector3 originTransform;
    [HideInInspector]
    public Transform imageToChange;

    public GameObject componentPrefab;
    public string componentName;
    public GameObject tempComponent;

    void Start()
    {
        hoverGroup.Subscribe(this);
        imageToChange = transform.GetChild(0);
        originTransform = imageToChange.transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        hoverGroup.OnTabSelected(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        hoverGroup.OnTabExit(this);
    }
}
