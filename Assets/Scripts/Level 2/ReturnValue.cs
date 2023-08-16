using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReturnValue : MonoBehaviour
{
    public int indexValue;
    public SelectedComponent selectedComponentBtn;
    public Connection connection;
    public bool pressedBtn;
    Color greenish = new Color(0, 255, 0, 143);
    Color whiter = new Color(255, 255, 255, 143);

    private void Start()
    {
        connection = FindObjectOfType<Connection>();
        pressedBtn = false;
        indexValue = 0;


        foreach (Button btn in transform.GetComponentsInChildren<Button>())
        {
            btn.onClick.AddListener(delegate { ReturnConnectionPoint(); });
        }
        selectedComponentBtn = connection.selectedComponent;

        foreach (GameObject t in selectedComponentBtn.selectedTransform)
        {
            //Debug.Log("Reached");
            var mesh = t.GetComponent<MeshRenderer>().sharedMaterial;
            //Debug.Log(connection.selectionMat + "+" + mesh);
            Debug.Log(t.name.Length);
            Debug.Log(int.Parse(t.name.Substring(t.name.Length-1,1)) + "This is the Index Number");
            //Debug.Log(mesh);
            //Debug.Log(connection.selectionMat);
            if (mesh == connection.selectionMat)
            {
                int index = selectedComponentBtn.IndexReturning(t);
                if (index == int.Parse(GetComponentInChildren<Button>().gameObject.name))
                {
                    pressedBtn = true;
                    Debug.Log("these two indexes are the same");
                    gameObject.transform.GetChild(index-1).GetComponent<Image>().color = greenish;
                }
            }
        }
    }


    public void ReturnConnectionPoint()
    {
        
        if (connection.pipeWarning)
        {
            connection.pipeWarningPanel.SetActive(true);
            return;
        }
        pressedBtn = !pressedBtn;

        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color != greenish)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = greenish;
            indexValue = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        }
        else if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color != whiter)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = whiter;
            indexValue = 0;//int.Parse(EventSystem.current.currentSelectedGameObject.name);
        }
        scriptRef();
    }

    public int ReturnIndex() 
    {
        connection.valueReturnBtn = this;
        Debug.Log(indexValue);
        return indexValue;
    }

    private void scriptRef() 
    {
        if (pressedBtn)
        {
            connection.valueReturnBtn = this;
        }
    }


    //public void ButtonPress1()
    //{
    //    if (connection.pipeWarning)
    //    {
    //        connection.pipeWarningPanel.SetActive(true);
    //        return;
    //    }
    //    if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color == Color.green)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.green;
    //    }
    //    pressedBtn = !pressedBtn;
    //    indexValue = 1;
    //    scriptRef();
    //}

    //public void ButtonPress2()
    //{
    //    if (connection.pipeWarning)
    //    {
    //        connection.pipeWarningPanel.SetActive(true);
    //        return;
    //    }
    //    if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color == Color.green)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.green;
    //    }
    //    pressedBtn = !pressedBtn;
    //    indexValue = 2;
    //    scriptRef();
    //}

    public void ButtonPress3()
    {
        indexValue = 3;
    }

    //public void ButtonPress4()
    //{
    //    if (connection.pipeWarning)
    //    {
    //        connection.pipeWarningPanel.SetActive(true);
    //        return;
    //    }
    //    if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color == Color.green)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.green;
    //    }
    //    pressedBtn = !pressedBtn;
    //    indexValue = 4;
    //    scriptRef();
    //}
}
