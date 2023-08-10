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

    private bool canChange = false;

    private void Start()
    {
        connection = FindObjectOfType<Connection>();
    }

    public void ShowUI(Vector3 selectionPoint) 
    {
        if (!uiTemp)
        {
            Debug.Log(selectionPoint);
            connection.uiParent.transform.position = selectionPoint;
            uiTemp = Instantiate(UIPrefab, selectionPoint, Quaternion.identity, connection.uiParent.transform);
            Debug.Log(selectionPoint + "round 2");
            uiTemp.transform.localPosition = Vector3.zero;
            Camera.main.transform.parent.GetComponent<CameraMovement>().enabled = false;
            canChange = false;
            StartCoroutine(Co());
        }
        valueReturn = uiTemp.GetComponent<ReturnValue>();
    }

    public void RemoveUI() 
    {
        if (canChange)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Camera.main.transform.parent.GetComponent<CameraMovement>().enabled = true;
        }
        Destroy(uiTemp);
    }

    public GameObject IndexReturn() 
    {
        if (valueReturn.ReturnIndex() == 0)
            return null;
        connectionPoint = selectedTransform[valueReturn.ReturnIndex()-1];
        return connectionPoint;
    }

    private IEnumerator Co()
    {
        yield return new WaitForEndOfFrame();
        canChange = true;
    }
}
