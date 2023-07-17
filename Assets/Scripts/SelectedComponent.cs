using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedComponent : MonoBehaviour
{
    public bool selected { get; private set; }
    public ReturnValue valueReturn;
    public GameObject UIPrefab;
    public GameObject canvas;
    public GameObject uiTemp;
    [SerializeField]
    private List<GameObject> selectedTransform = new List<GameObject>();
    private GameObject connectionPoint;


    public void ShowUI() 
    {

        if (!valueReturn.gameObject.activeInHierarchy)
        {
            valueReturn.gameObject.transform.position = new Vector2(Input.GetTouch(0).position.x - 50, Input.GetTouch(0).position.y);
        }
        uiTemp = Instantiate(UIPrefab, new Vector2(Input.GetTouch(0).position.x - 50, Input.GetTouch(0).position.y), Quaternion.identity);
        uiTemp.transform.parent = canvas.transform;
    }

    public void RemoveUI() 
    {
        valueReturn.gameObject.SetActive(false);
        Destroy(uiTemp);
    }

    public GameObject IndexReturn() 
    {
        if (valueReturn.ReturnIndex() == 0)
            return null;
        connectionPoint = selectedTransform[valueReturn.ReturnIndex()-1];
        return connectionPoint;
    }
}
