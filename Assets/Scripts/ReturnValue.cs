using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnValue : MonoBehaviour
{
    public int indexValue;
    public SelectedComponent selectedComponentBtn;
    public bool pressedBtn;

    private void Start()
    {
        pressedBtn = false;
        indexValue = 0;
        gameObject.SetActive(false);
    }
    public void ButtonPress0() 
    {
        pressedBtn = !pressedBtn;
        indexValue = 1;
    }

    public void ButtonPress1() 
    {
        pressedBtn = !pressedBtn;
        indexValue = 2;
    }

    public int ReturnIndex() 
    {
        return indexValue;
    }
}
