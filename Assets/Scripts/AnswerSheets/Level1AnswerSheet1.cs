using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level1AnswerSheet1 : MonoBehaviour
{
    public List<GameObject> correctSelection1;
    public List<GameObject> correctSelection2;
    public List<GameObject> incorrectSelection;
    public GameObject correctPanel;
    public GameObject wrongPanel;

    public bool coolingTower;
    public bool ahu;
    public bool chiller;
    public bool cwp_opt;
    public bool cwp_opt_Elav;

    public bool wrongSelection;

    // Update is called once per frame
    void Update()
    {
        PlacementCheck();
    }

    void PlacementCheck()
    {
        if (correctSelection1[0].transform.childCount > 0 &&
            correctSelection1[0].transform.GetChild(0).CompareTag("Component/CoolingTower"))
        {
            coolingTower = true;
        }
        else
        {
            coolingTower = false;
        }

        if (correctSelection2.All(i => i.transform.childCount > 0) && 
            correctSelection2.All(i => i.transform.GetChild(0).CompareTag("Component/AHU")))
        {
            ahu = true;
        }
        else
        {
            ahu = false;
        }

        if(correctSelection1[1].transform.childCount > 0 &&
            correctSelection1[1].transform.GetChild(0).CompareTag("Component/Chiller"))
        {
            chiller = true;
        }
        else
        {
            chiller = false;
        }

        if (correctSelection1[2].transform.childCount > 0 &&
            correctSelection1[2].transform.GetChild(0).CompareTag("Component/CwpOpt"))
        {
            cwp_opt = true;
        }
        else if(correctSelection1[2].transform.childCount > 0 &&
            correctSelection1[2].transform.GetChild(0).CompareTag("Component/CwpOptElavated"))
        {
            cwp_opt_Elav = true;
        }
        else
        {
            cwp_opt_Elav = false;
            cwp_opt = false;
        }

        if(correctSelection1[3].transform.childCount > 0 &&
            correctSelection1[3].transform.GetChild(0).CompareTag("Component/CwpOptElavated"))
        {
            cwp_opt_Elav = true;
        }
        else if (correctSelection1[3].transform.childCount > 0 &&
            correctSelection1[3].transform.GetChild(0).CompareTag("Component/CwpOpt"))
        {
            cwp_opt = true;
        }
        else
        {
            cwp_opt = false;
            cwp_opt_Elav = false;
        }

        if (incorrectSelection.Any(i => i.transform.childCount > 0))
        {
            wrongSelection = true;
        }
        else
        {
            wrongSelection = false;
        }
    }

    public void AnswerCheck() 
    {
        if (coolingTower && ahu && chiller && cwp_opt && cwp_opt_Elav && !wrongSelection)
        {
            correctPanel.SetActive(true);
            Debug.Log("Correct");
        }
        else 
        {
            wrongPanel.SetActive(true);
            Debug.Log("Wrong");
        }
    }
}
