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

    private void Start()
    {
        connection = FindObjectOfType<Connection>();
        pressedBtn = false;
        indexValue = 0;

        /*
        foreach (Button btn in transform.GetComponentsInChildren<Button>())
        {
            btn.onClick.AddListener(delegate { ReturnConnectionPoint(); });
        }
        */
    }

    public void ButtonPress1()
    {
        if (connection.pipeWarning)
        {
            connection.pipeWarningPanel.SetActive(true);
            return;
        }

        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color == Color.green)
        {
            return;
        }
        else EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.green;
        indexValue = 1;
        scriptRef();
    }

    public void ButtonPress2()
    {
        if (connection.pipeWarning)
        {
            connection.pipeWarningPanel.SetActive(true);
            return;
        }
        pressedBtn = !pressedBtn;
        indexValue = 2;
        scriptRef();
    }

    public void ButtonPress3()
    {
        if (connection.pipeWarning)
        {
            connection.pipeWarningPanel.SetActive(true);
            return;
        }
        pressedBtn = !pressedBtn;
        indexValue = 3;
        scriptRef();
    }

    public void ButtonPress4()
    {
        if (connection.pipeWarning)
        {
            connection.pipeWarningPanel.SetActive(true);
            return;
        }
        pressedBtn = !pressedBtn;
        indexValue = 4;
        scriptRef();
    }

    /*
    private void ReturnConnectionPoint() 
    {
        if (connection.pipeWarning)
        {
            connection.pipeWarningPanel.SetActive(true);
            return;
        }
        pressedBtn = !pressedBtn;

        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color == Color.green)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.white;
            indexValue = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        }
        else 
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.green;
            indexValue = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        }
        scriptRef();
    }
    */
    public int ReturnIndex() 
    {
        connection.valueReturnBtn = this;
        return indexValue;
    }

    private void scriptRef() 
    {
        if (pressedBtn)
        {
            connection.valueReturnBtn = this;
        }
    }

    
}
