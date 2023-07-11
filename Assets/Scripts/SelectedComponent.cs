using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedComponent : MonoBehaviour
{
    public bool selected { get; private set; }
    public ReturnValue valueReturn;
    [SerializeField]
    private List<GameObject> selectedTransform = new List<GameObject>();
    private GameObject connectionPoint;


    public void ShowUI() 
    {
        valueReturn.gameObject.SetActive(true);
        valueReturn.gameObject.transform.position = new Vector2(Input.GetTouch(0).position.x - 50, Input.GetTouch(0).position.y);
    }

    public void RemoveUI() 
    {
        valueReturn.gameObject.SetActive(false);
    }

    public GameObject IndexReturn() 
    {
        if (valueReturn.ReturnIndex() == 0)
            return null;
        connectionPoint = selectedTransform[valueReturn.ReturnIndex()-1];
        return connectionPoint;
    }
}
