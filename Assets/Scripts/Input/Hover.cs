using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level3;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Hover : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    bool hoverOver;
    [HideInInspector]
    public bool hoverTab;
    Vector3 startSize;
    public bool isTab;
    public float moveSpeed;
    public static bool componentSelected;
    public bool buttonLetgo;
    private bool tabbing;

    public Vector3 startPosition;
    public Vector3 desiredPosition;
    public Vector3 desiredSize;
    private Vector3 mousePos;

    public GameObject component;
    public GameObject pipe, pipeBody, pipeEntrance;
    public GameObject componentName;

    Placement placement;
    Connection connection;
    CameraMovement cameraMovement;
    [HideInInspector]
    public GameObject componentPrefab;

    public TextMeshProUGUI test;

    private bool isLevel3 = false;

    void Start()
    {
        placement = FindObjectOfType<Placement>();
        connection = FindObjectOfType<Connection>();
        cameraMovement = FindObjectOfType<CameraMovement>();

        //transform.localPosition = startPosition;
        //startPosition = transform.localPosition;
        startSize = transform.localScale;

        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            isLevel3 = true;
        }
    }
    void Update()
    {
        //Debug.Log(transform.localPosition.x + "," + desiredPosition.x + "," + (transform.localPosition.x >= desiredPosition.x));
        if (test)
        {
            test.text = hoverTab.ToString();
        }

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
        //checks if the gameobject is the tab itself
        {
            if (hoverTab)
            //triggers the tab moving in and out
            {
                if (transform.localPosition.x < desiredPosition.x)
                {
                    //transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime * 5;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition * 0.9f, Time.deltaTime* moveSpeed);
                    if (transform.localPosition.x >= desiredPosition.x)
                    {
                        transform.localPosition = desiredPosition;
                        tabbing = true;

                    }
                }
            }
            else
            {
                if (transform.localPosition.x > startPosition.x)
                {
                    //transform.position -= new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime * 5;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition * 1.1f, Time.deltaTime * moveSpeed);
                    if (transform.localPosition.x <= startPosition.x) 
                    {
                        transform.localPosition = startPosition;
                        tabbing = false;

                    }
                }
            }
        }

        if (Input.touchCount == 0)
        {
            if (isLevel3)
                return;
            cameraMovement.hover = null;
            componentSelected = false;
            Destroy(componentPrefab);
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
        if (tabbing == false)
        {
            hoverTab = true;
        }
    }

    public void TabExit()
    {
        if (tabbing == true)
        {
            hoverTab = false;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    //handles instantiting the object component to be placed in the scene
    {
        if (componentSelected || isLevel3)
            return;

        buttonLetgo = false;

        if (placement)
        {
            cameraMovement.hover = placement.hover = this;
            placement.component = null;
        }
        else 
        {
            cameraMovement.hover = this;
        }

        mousePos = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 150) );
        if (component && !componentPrefab)
        {
            componentSelected = true;
            placement.selectedPrefab = component;
            component.GetComponent<Collider>().enabled = false;
            componentPrefab = Instantiate(component, mousePos, Quaternion.identity);
        }
        else if (pipe && connection) 
        {
            connection.pipe = pipe;
            connection.entrance = pipeEntrance;
            connection.exit = pipeEntrance;
            connection.body = pipeBody;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (component == null || componentPrefab == null || isLevel3)
            return;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 150));
        componentPrefab.transform.position = mousePos;
    }

    public void Selection()
    {
        if (isLevel3)
            return;
        if (!isTab && placement != null)
        {
            placement.selectedPrefab = component;
        }
    }

    public bool SetHoverTab
    {
        set { hoverTab = value; }
    }
}
