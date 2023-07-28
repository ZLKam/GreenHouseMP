using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverGroup : MonoBehaviour, IPointerClickHandler
{
    public List<HoverTab> componentTabs;
    public Placement placement;
    public TextMeshProUGUI wheelTitle;

    private Vector3 mousePos;

    [SerializeField]
    private bool openTab;

    public void Subscribe(HoverTab components)
    {
        if (componentTabs == null)
        {
            componentTabs = new List<HoverTab>();
        }

        componentTabs.Add(components);
    }

    private void Update()
    {
        if (openTab)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-250, 0f, 0f), Time.deltaTime * 2);
        }
        else 
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-390, 0f, 0f), Time.deltaTime * 2);
        }
    }

    public void OnTabExit(HoverTab components)
    //After clicking the tabs
    {
        components.imageToChange.transform.localScale = components.originTransform;
    }

    public void OnTabSelected(HoverTab components)
    //Upon Clicking the tabs
    {
        components.imageToChange.transform.localScale = components.originTransform;
        components.imageToChange.transform.localScale = Vector3.Lerp(components.imageToChange.transform.localScale, components.imageToChange.transform.localScale * 0.8f, 1);
        wheelTitle.text = components.componentName;
        if (placement)
        {
            placement.component = null;
            placement.component = components.componentPrefab;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        openTab = !openTab;
        wheelTitle.text = "";
    }

}
