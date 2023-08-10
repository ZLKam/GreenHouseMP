using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void ButtonPress1() 
    {
        if (connection.pipeWarning)
        {
            connection.pipeWarningPanel.SetActive(true);
            return;
        }
        pressedBtn = !pressedBtn;
        if (pressedBtn)
        {
            //button
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else gameObject.GetComponent<Image>().color = Color.white;
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
