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
        linePathFind.imgColorSelected.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        linePathFind.typeOfLineSelected = true;
        linePathFind.colorOfLineSelected = transform.GetChild(0).GetComponent<Image>().color;
    }
}
