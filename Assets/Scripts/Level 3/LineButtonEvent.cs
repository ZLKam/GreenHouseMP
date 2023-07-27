using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineButtonEvent : MonoBehaviour, IPointerClickHandler
{
    public LinePathFind linePathFind;

    public void OnPointerClick(PointerEventData eventData)
    {
        linePathFind.typeOfLineSelected = true;
        linePathFind.colorOfLineSelected = transform.GetChild(0).GetComponent<Image>().color;
    }
}
