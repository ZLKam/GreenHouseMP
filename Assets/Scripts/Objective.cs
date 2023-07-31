using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameObject objectivePanel;
    public GameObject Main;


    public void Continue()
    {
        objectivePanel.SetActive(false);
        Main.SetActive(true);
    }


}
