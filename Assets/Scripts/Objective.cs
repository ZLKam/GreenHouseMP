using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameObject obj;
    public GameObject Menu;

    public void Continue()
    {
        obj.SetActive(false);
        Menu.SetActive(true);
    }
}
