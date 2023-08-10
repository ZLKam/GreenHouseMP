using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SelectedComponent : MonoBehaviour
{
    public bool selected { get; private set; }
    private ReturnValue valueReturn;
    public GameObject UIPrefab;
    public GameObject canvas;
    public GameObject uiTemp;
    [SerializeField]
    private List<GameObject> selectedTransform = new List<GameObject>();
    private GameObject connectionPoint;

    public ToggleMultiConnect multiConnecta;
    public Connection connection;
    private CameraMovement cameraController;


    private void Start()
    {
        connection = FindObjectOfType<Connection>();
        cameraController = FindObjectOfType<CameraMovement>();
    }

    public void ShowUI(Vector3 selectionPoint) 
    {
        if (!uiTemp)
        {
            uiTemp = Instantiate(UIPrefab, selectionPoint, Quaternion.identity, connection.uiParent.transform);
            uiTemp.transform.localPosition = Vector3.zero;
        }
        valueReturn = uiTemp.GetComponent<ReturnValue>();
    }

    public void RemoveUI() 
    {
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
