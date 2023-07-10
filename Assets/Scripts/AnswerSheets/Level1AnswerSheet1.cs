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

    public Shader shaderRight;
    public Shader shaderWrong;


    // Update is called once per frame
    void Update()
    {
        //PlacementCheck();
    }

    public void AnswerCheck()
    {
        PlacementCheck();
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

    void PlacementCheck()
    {
        if (correctSelection1[0].transform.childCount > 0 &&
            correctSelection1[0].transform.GetChild(1).CompareTag("Component/CoolingTower"))
        {
            coolingTower = true;
        }
        else
        {
            coolingTower = false;
        }

        if (correctSelection2.All(i => i.transform.childCount > 0) && 
            correctSelection2.All(i => i.transform.GetChild(1).CompareTag("Component/AHU")))
        {
            ahu = true;
        }
        else
        {
            ahu = false;
        }

        if(correctSelection1[1].transform.childCount > 0 &&
            correctSelection1[1].transform.GetChild(1).CompareTag("Component/Chiller"))
        {
            chiller = true;
        }
        else
        {
            chiller = false;
            
        }

        if (correctSelection1[2].transform.childCount > 0 &&
            correctSelection1[2].transform.GetChild(1).CompareTag("Component/CwpOpt") && !cwp_opt)
        {
            Debug.Log("test");
            cwp_opt = true;
        }
        else if(correctSelection1[2].transform.childCount > 0 &&
            correctSelection1[2].transform.GetChild(1).CompareTag("Component/CwpOptElavated") && !cwp_opt_Elav)
        {
            cwp_opt_Elav = true;
        }
        else
        {
            Debug.Log("test");  
            cwp_opt_Elav = false;
            cwp_opt = false;
        }

        if(correctSelection1[3].transform.childCount > 0 &&
            correctSelection1[3].transform.GetChild(1).CompareTag("Component/CwpOptElavated") && !cwp_opt_Elav)
        {
            cwp_opt_Elav = true;
        }
        else if (correctSelection1[3].transform.childCount > 0 &&
            correctSelection1[3].transform.GetChild(1).CompareTag("Component/CwpOpt") && !cwp_opt)
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

    //private void SetShaderColor(Transform gameobjectToChange, bool correct = false) 
    //{
    //    //Finds the child in the model and sets the shader of the child(main gameobject dont have renderer)
    //    if (gameobjectToChange.childCount > 0) 
    //    {
    //        childrenInModel = gameobjectToChange.GetComponentsInChildren<Transform>();
    //        foreach (Transform child in childrenInModel)
    //        {
    //            if (child.GetComponent<Renderer>() != null) 
    //            {
    //                childRenderList.Add(child.GetComponent<Renderer>());
    //                for (int i = 0; i < childRenderList.Count; i++) 
    //                {
    //                    if (correct)
    //                    {
    //                        childRenderList[i].sharedMaterial.shader = shaderRight;
    //                        childRenderList[i].sharedMaterial.SetColor("_OutlineColor", Color.green);
    //                        childRenderList[i].sharedMaterial.SetFloat("_OutlineWidth", 1.20f);
    //                    }
    //                    else 
    //                    {
    //                        childRenderList[i].sharedMaterial.shader = shaderWrong;
    //                        childRenderList[i].sharedMaterial.SetColor("_OutlineColor", Color.red);
    //                        childRenderList[i].sharedMaterial.SetFloat("_OutlineWidth", 1.20f);
    //                    }
    //                }
    //            }
    //        }
    //    }
        //else if (correct)
        //{
        //    gameobjectToChange.GetComponent<Renderer>().material.shader = shaderRight;
        //    gameobjectToChange.GetComponent<Renderer>().sharedMaterial.SetColor("_OutlineColor", Color.green);
        //}
        //else 
        //{
        //    gameobjectToChange.GetComponent<Renderer>().material.shader = shaderWrong;
        //    gameobjectToChange.GetComponent<Renderer>().sharedMaterial.SetColor("_OutlineColor", Color.red);
        //}
    //}

}
