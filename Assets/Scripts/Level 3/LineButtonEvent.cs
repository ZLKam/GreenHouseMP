using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineButtonEvent : MonoBehaviour, IPointerClickHandler
{
    public LinePathFind linePathFind;
    public string nameOfLine;

    private bool clicked = false;

    [SerializeField]
    private TextMeshProUGUI txtName;
    [SerializeField]
    private Button otherLine;

    private void Start()
    {
        txtName = transform.parent.GetChild(transform.parent.childCount - 1).GetComponent<TextMeshProUGUI>();
        List<Button> childrenButton = transform.parent.GetComponentsInChildren<Button>().ToList();
        for (int i = 0; i < childrenButton.Count; i++)
        {
            if (childrenButton[i].gameObject != gameObject)
            {
                otherLine = childrenButton[i];
                break;
            }
        }
    }

    private void OnDisable()
    {
        GetComponent<Image>().color = new Color32(83, 98, 115, 255);
        txtName.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>()?.Play("Click");
        if (linePathFind.IsFindingPath())
            return;
        /// On clicked, it will check if the line is already selected or not.
        /// When type is not selected, it will set the line to selected.
        if (!linePathFind.typeOfLineSelected)
        {
            linePathFind.imgColorSelected.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
            linePathFind.typeOfLineSelected = true;
            linePathFind.colorOfLineSelected = transform.GetChild(0).GetComponent<Image>().color;
            GetComponent<Image>().color = Color.green;
            txtName.text = nameOfLine;
            clicked = true;
        }
        else
        {
            /// When the type of line is selected, it will check if the other type is selected
            /// If the other type is selected, it will set the other type to not selected.
            /// If the other type is not selected, it means that the selected type is the one that is clicked.
            /// Then, it will set the selected type to not selected.
            if (!otherLine.GetComponent<LineButtonEvent>().clicked)
            {
                linePathFind.imgColorSelected.sprite = linePathFind.transparentSprite;
                linePathFind.typeOfLineSelected = false;
                linePathFind.colorOfLineSelected = new Color(0, 0, 0, 0);
                GetComponent<Image>().color = new Color32(83, 98, 115, 255);
                txtName.text = "";
                clicked = false;
            }
            else
            {
                linePathFind.imgColorSelected.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
                linePathFind.typeOfLineSelected = true;
                linePathFind.colorOfLineSelected = transform.GetChild(0).GetComponent<Image>().color;
                GetComponent<Image>().color = Color.green;
                txtName.text = nameOfLine;
                clicked = true;
                otherLine.GetComponent<Image>().color = new Color32(83, 98, 115, 255);
                otherLine.GetComponent<LineButtonEvent>().clicked = false;
            }
        }
    }
}
