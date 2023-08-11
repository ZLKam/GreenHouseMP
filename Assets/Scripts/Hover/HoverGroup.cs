using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HoverGroup : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [Header("Script References")]
    [SerializeField]
    private Placement1 placement;
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

    public Image tab;

    private bool openTab;
    private bool placeComponent;
    private Vector3 mousePos;
    public bool dragToPlace;

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
        openTab = true;
    }

    public void OnTabSelected(HoverTab components)
    //Upon Clicking the tabs
    {
        dragToPlace = true;
        cameraController.allowZoom = false;
        ResetTabColor(components);
        components.imageToChange.transform.localScale = components.originTransform;
        components.imageToChange.transform.localScale = Vector3.Lerp(components.imageToChange.transform.localScale, components.imageToChange.transform.localScale * 0.8f, 1);

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
        dragToPlace = true;
        cameraController.allowZoom = false;
        if (placement.component)
        {
#if UNITY_STANDALONE
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 150));
#endif
#if UNITY_ANDROID
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.nearClipPlane + 150));
#endif
            placement.component.transform.position = mousePos;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (dragToPlace)
            return;
        openTab = !openTab;
    }

    private void OnPlacementFound(HoverTab components) 
    {
        placement.selectedPrefab = placement.component = null;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 150));
        components.componentPrefab.GetComponent<Collider>().enabled = false;
        placement.component = Instantiate(components.componentPrefab, mousePos, Quaternion.identity);
        placement.selectedPrefab = components.componentPrefab;
    }

    private void OnConnectionFound(HoverTab components) 
    {
        connection.pipe = components.parentPipe;
        connection.exit = connection.entrance = components.pipeEntrance;
        connection.body = components.pipeBody;
    }

    public void ResetTabColor(HoverTab component) 
    {
        foreach (HoverTab hovertab in componentTabs) 
        {
            if (hovertab != component)
            {
                hovertab.backgroundImage.color = Color.white;
            }
        }
    }

    #region MonoBehaviour

    private void Start()
    {
        cameraController = FindAnyObjectByType<CameraMovement>();
        placement = FindObjectOfType<Placement1>();
        connection = FindObjectOfType<Connection>();
    }

    private void Update()
    {
        if (openTab)
        {
            transform.GetComponent<RectTransform>().localPosition = Vector3.Lerp(transform.GetComponent<RectTransform>().localPosition, new Vector3(finalPositionOfWheel, transform.GetComponent<RectTransform>().localPosition.y, transform.GetComponent<RectTransform>().localPosition.z), moveSpeed * Time.deltaTime);
            tab.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 90);
            placeComponent = true;
        }
        else
        {
            transform.GetComponent<RectTransform>().localPosition = Vector3.Lerp(transform.GetComponent<RectTransform>().localPosition, new Vector3(startPositionOfWheel, transform.GetComponent<RectTransform>().localPosition.y, transform.GetComponent<RectTransform>().localPosition.z), moveSpeed * Time.deltaTime);
            tab.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 90);
            placeComponent = false;
        }

        if(Input.touchCount == 0 && dragToPlace) 
        {
            dragToPlace = false;
            if(cameraController)
                cameraController.allowZoom = false;
        }

        if (placement)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && placement.component)
            {
                if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out RaycastHit hit, Mathf.Infinity, ~(1 << 6)))
                {
                    Destroy(placement.component);
                }
                else if (hit.transform.CompareTag("Untagged"))
                {
                    Destroy(placement.component);
                }
            }
        }
    }

    #endregion

}
