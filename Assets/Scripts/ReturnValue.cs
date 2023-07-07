using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        gameObject.SetActive(false);
    }
    public void ButtonPress1() 
    {
        pressedBtn = !pressedBtn;
        indexValue = 1;
        scriptRef();
    }

    public void ButtonPress2() 
    {
        pressedBtn = !pressedBtn;
        indexValue = 2;
        scriptRef();
    }

    public void ButtonPress3()
    {
        pressedBtn = !pressedBtn;
        indexValue = 3;
        scriptRef();
    }

    public void ButtonPress4()
    {
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
