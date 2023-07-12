using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level3;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    bool hoverOver;
    bool hoverTab;
    Vector3 startSize;
    public bool isTab;
    public float moveSpeed;
    public bool componentSelected;

    public Vector3 startPosition;
    public Vector3 desiredPosition;
    public Vector3 desiredSize;
    private Vector3 mousePos;

    public GameObject component;
    public GameObject componentName;

    Placement placement;
    GameObject componentPrefab;
    CameraMovement cameraMovement;
    

    void Start()
    {
        placement = FindObjectOfType<Placement>();
        cameraMovement = FindObjectOfType<CameraMovement>();

        //transform.localPosition = startPosition;
        //startPosition = transform.localPosition;
        startSize = transform.localScale;
    }
    void Update()
    {
        if (hoverOver)
        {
            if(transform.localScale.x < desiredSize.x)
            {
                transform.localScale += new Vector3(.1f, .1f, .1f);
            }
        }
        else
        {
            if (transform.localScale.x > startSize.x)
            {
                transform.localScale -= new Vector3(.1f, .1f, .1f);
            }
        }

        if (isTab)
        {
            if (hoverTab)
            {
                if (transform.localPosition.x < desiredPosition.x)
                {
                    //transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime * 5;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, Time.deltaTime* moveSpeed);
                }
            }
            else
            {
                if (transform.localPosition.x > startPosition.x)
                {
                    //transform.position -= new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime * 5;
                    transform.localPosition = Vector3.Lerp(startPosition, transform.localPosition, Time.deltaTime * moveSpeed);
                }
            }
        }
    }
    public void Enter()
    {
        hoverOver = true;
        if (componentName != null)
        {
            componentName.SetActive(true);
        }
        transform.SetSiblingIndex(transform.parent.childCount - 1);
    }

    public void Exit()
    {
        //Debug.Log("exit");
        hoverOver = false;
        if (componentName != null)
        {
            componentName.SetActive(false);
        }
    }

    public void TabEnter()
    {
        hoverTab = true;
    }

    public void TabExit()
    {
        hoverTab = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    //handles instantiting the object component to be placed in the scene
    {
        Debug.Log("press");
        cameraMovement.hover = this;
        componentSelected = true;
        placement.selectedPrefab = component;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        componentPrefab = Instantiate(component, mousePos, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (component == null || componentPrefab == null)
            return;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 50));
        componentPrefab.transform.position = mousePos;
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        cameraMovement.hover = null;
    }


    public void Selection()
    {
        if (!isTab && placement != null)
        {
            Debug.Log("component selected");
            placement.selectedPrefab = component;
        }
    }
}
