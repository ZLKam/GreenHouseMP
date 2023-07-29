using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverGroup : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [Header("Script References")]
    [SerializeField]
    private Placement placement;
    [SerializeField]
    private CameraMovement cameraController;
    [SerializeField]
    private Connection connection;

    [Header("Adjustable Values")]
    public int moveSpeed;
    [SerializeField]
    private float finalPositionOfWheel;
    [SerializeField]
    private float startPositionOfWheel;

    public TextMeshProUGUI wheelTitle;
    public List<HoverTab> componentTabs;

    private bool openTab;
    private bool placeComponent;
    private Vector3 mousePos;

    public void Subscribe(HoverTab components)
    {
        if (componentTabs == null)
        {
            componentTabs = new List<HoverTab>();
        }

        componentTabs.Add(components);
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

        wheelTitle.text = components.componentName;

        if (placement && placeComponent)
        //For level 1
        {
            OnPlacementFound(components);
        }
        else if (connection) 
        //For level 2
        {
            OnConnectionFound(components);
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
    }

    private void OnPlacementFound(HoverTab components) 
    {
        placement.selectedPrefab = placement.component = null;
        placement.selectedPrefab = components.componentPrefab;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 150));
        components.componentPrefab.GetComponent<Collider>().enabled = false;
        placement.component = Instantiate(components.componentPrefab, mousePos, Quaternion.identity);
    }

    private void OnConnectionFound(HoverTab components) 
    {
        connection.pipe = components.parentPipe;
        connection.exit = connection.entrance = components.pipeEntrance;
        connection.body = components.pipeBody;
    }

    #region MonoBehaviour

    private void Start()
    {
        cameraController = FindAnyObjectByType<CameraMovement>();
        placement = FindObjectOfType<Placement>();
        connection = FindObjectOfType<Connection>();
    }

    private void Update()
    {
        if (openTab)
        {
            transform.GetComponent<RectTransform>().localPosition = Vector3.Lerp(transform.GetComponent<RectTransform>().localPosition, new Vector3(finalPositionOfWheel, transform.GetComponent<RectTransform>().localPosition.y, transform.GetComponent<RectTransform>().localPosition.z), moveSpeed * Time.deltaTime);
            placeComponent = true;
        }
        else
        {
            transform.GetComponent<RectTransform>().localPosition = Vector3.Lerp(transform.GetComponent<RectTransform>().localPosition, new Vector3(startPositionOfWheel, transform.GetComponent<RectTransform>().localPosition.y, transform.GetComponent<RectTransform>().localPosition.z), moveSpeed * Time.deltaTime);
            placeComponent = false;
        }
    }

    #endregion


}
