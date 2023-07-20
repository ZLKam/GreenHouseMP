using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public void ShowUI(Transform selectionPoint) 
    {
        if (!uiTemp)
        {
            uiTemp = Instantiate(UIPrefab, selectionPoint.position, Quaternion.identity);
            Camera.main.transform.parent.GetComponent<CameraMovement>().enabled = false;
            canChange = false;
            StartCoroutine(Co());
        }
        uiTemp.transform.SetParent(canvas.transform);
        valueReturn = uiTemp.GetComponent<ReturnValue>();
}

    public void RemoveUI() 
    {
        if (canChange)
        {
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
