using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverGroup : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public List<HoverTab> componentTabs;
    public Placement placement;
    public CameraMovement cameraController;
    public TextMeshProUGUI wheelTitle;

    private Vector3 mousePos;
    [SerializeField]
    private float finalPosition;
    [SerializeField]
    private float startPosition;
    private float postionToPassIn;

    [SerializeField]
    private bool openTab;
    private bool placeComponent;

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
            transform.GetComponent<RectTransform>().position = Vector3.Lerp(transform.GetComponent<RectTransform>().position, new Vector3(postionToPassIn, transform.GetComponent<RectTransform>().position.y, transform.GetComponent<RectTransform>().position.z), Time.deltaTime * 2);
            placeComponent = true;
        }
        else 
        {
            transform.GetComponent<RectTransform>().position = Vector3.Lerp(transform.GetComponent<RectTransform>().position, new Vector3(startPosition, transform.GetComponent<RectTransform>().position.y, transform.GetComponent<RectTransform>().position.z), Time.deltaTime * 2);
            placeComponent = false;
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
        cameraController.allowRotation = false;
        

        if (placement && placeComponent)
        {
            wheelTitle.text = components.componentName;
            placement.selectedPrefab = null;
            placement.component = null;
            placement.selectedPrefab = components.componentPrefab;
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 150));
            components.componentPrefab.GetComponent<Collider>().enabled = false;
            placement.component = Instantiate(components.componentPrefab, mousePos, Quaternion.identity);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 150));
        placement.component.transform.position = mousePos;
        placement.Delete(placement.component.transform);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        openTab = !openTab;
        cameraController = FindAnyObjectByType<CameraMovement>();
        wheelTitle.text = "";
        if (openTab)
        {
            postionToPassIn = transform.GetComponent<RectTransform>().position.x * finalPosition;
        }
    }

}
