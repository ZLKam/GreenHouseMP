using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTab : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public HoverGroup hoverGroup;

    public Vector3 originTransform;
    public Transform imageToChange;

    public GameObject componentPrefab;
    public GameObject[] pipes;
    public string componentName;
    
    [HideInInspector]
    public GameObject parentPipe, pipeBody, pipeEntrance;
    [HideInInspector]
    public Image backgroundImage;


    void Start()
    {
        hoverGroup.Subscribe(this);
        imageToChange = transform.GetChild(0);
        originTransform = imageToChange.transform.localScale;

        PipeAssign();

        backgroundImage = transform.GetChild(0).GetComponent<Image>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        hoverGroup.OnTabSelected(this);
        backgroundImage.color = Color.green;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        hoverGroup.OnTabExit(this);
    }

    private void PipeAssign() 
    {
        if (pipes != null && pipes.Length > 0)
        {
            foreach (GameObject pipe in pipes)
            {
                if (pipe.name.Contains("body"))
                {
                    pipeBody = pipe;
                }
                else if (pipe.name.Contains("Final"))
                {
                    pipeEntrance = pipe;
                }
                else
                {
                    parentPipe = pipe;
                }
            }
            return;
        }
    }
}
