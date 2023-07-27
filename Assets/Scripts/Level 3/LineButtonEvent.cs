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
        FindObjectOfType<AudioManager>()?.Play("Click");
        if (linePathFind.IsFindingPath())
            return;
        linePathFind.typeOfLineSelected = true;
        linePathFind.colorOfLineSelected = transform.GetChild(0).GetComponent<Image>().color;
    }
}
