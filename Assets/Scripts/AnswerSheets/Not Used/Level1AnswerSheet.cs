using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1AnswerSheet : MonoBehaviour
{
    public GameObject[] SelectionPointsArray;
    public GameObject CorrectPanel;
    public GameObject WrongPanel;

    public bool coolingTower = false;
    public bool ahu = false;
    public bool chiller = false;
    public bool cwp_opt = false;
    public bool cwp_opt_Elav = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlacementChecker();
    }


    void PlacementChecker() 
    {
        //Here we are looping through the 18 selection points array we dragged and dropped into the inspector
        for (int i = 0; i < SelectionPointsArray.Length; i++)
        {
            //checks point 0 where the cooling tower is supose to be and checks if there is a child attached to the game object which in our case is the selection point 
            //if the selection point does have a child it checks if that child game objects tag is the same as we declared here if it is the same it returns the boolean as true
            //we repeat this process for the rest of the components >.O
            if (SelectionPointsArray[0].transform.childCount > 0)
            {
                GameObject childObject = SelectionPointsArray[0].transform.GetChild(0).gameObject;

                if (SelectionPointsArray[0].transform && childObject.CompareTag("Component/CoolingTower"))
                {
                    coolingTower = true;

                    Debug.Log(coolingTower + "Cooling Tower Correct");
                }


            }
            else
            {
                coolingTower = false;
            }

            if ((SelectionPointsArray[1].transform.childCount > 0 && SelectionPointsArray[2].transform.childCount > 0 && SelectionPointsArray[3].transform.childCount > 0 && SelectionPointsArray[4].transform.childCount > 0))
            {
                GameObject childObject = SelectionPointsArray[1].transform.GetChild(0).gameObject;
                GameObject childObject2 = SelectionPointsArray[2].transform.GetChild(0).gameObject;
                GameObject childObject3 = SelectionPointsArray[3].transform.GetChild(0).gameObject;
                GameObject childObject4 = SelectionPointsArray[4].transform.GetChild(0).gameObject;

                if (SelectionPointsArray[1].transform && childObject.CompareTag("Component/AHU") && SelectionPointsArray[2].transform && childObject2.CompareTag("Component/AHU") &&
                    SelectionPointsArray[3].transform && childObject3.CompareTag("Component/AHU") && SelectionPointsArray[4].transform && childObject4.CompareTag("Component/AHU"))
                {
                    ahu = true;

                    Debug.Log(ahu + "ahu Correct");
                }

            }
            else
            {
                ahu = false;
            }

            if (SelectionPointsArray[5].transform.childCount > 0)
            {
                GameObject childObject = SelectionPointsArray[5].transform.GetChild(0).gameObject;

                if (SelectionPointsArray[5].transform && childObject.CompareTag("Component/Chiller"))
                {
                    chiller = true;

                    Debug.Log(chiller + "Chiller Correct");
                }

            }
            else
            {
                chiller = false;
            }

            if (SelectionPointsArray[16].transform.childCount > 0)
            {
                GameObject childObject = SelectionPointsArray[16].transform.GetChild(0).gameObject;

                if (SelectionPointsArray[16].transform)
                {
                    if (childObject.CompareTag("Component/CwpOpt"))
                    {
                        cwp_opt = true;

                        Debug.Log(cwp_opt + "CwpOpt");
                    }
                    else if (childObject.CompareTag("Component/cwp_opt_Elavated")) 
                    {
                        cwp_opt_Elav = true;
                    }
                }

            }
            else
            {
                cwp_opt = false;
                cwp_opt_Elav = false;
            }


            if (SelectionPointsArray[17].transform.childCount > 0)
            {
                GameObject childObject = SelectionPointsArray[17].transform.GetChild(0).gameObject;

                if (SelectionPointsArray[17].transform)
                {
                    if (childObject.CompareTag("Component/CwpOpt"))
                    {
                        cwp_opt = true;
                    }
                    else if (childObject.CompareTag("Component/cwp_opt_Elavated"))
                    {
                        cwp_opt_Elav = true;
                        Debug.Log(cwp_opt_Elav + "cwp_opt_Elavated");
                    }
                }

            }
            else
            {
                cwp_opt = false;
                cwp_opt_Elav = false;
            }

        }
    }

    public void AnswerCheck() 
    {
        if (coolingTower && ahu && chiller && cwp_opt && cwp_opt_Elav)
        {
            CorrectPanel.SetActive(true);
            Debug.Log("Correct");
        }
        else 
        {
            StartCoroutine(DisablePanelAfterDelay(1f));
            Debug.Log("Wrong");
        }
    }


    private IEnumerator DisablePanelAfterDelay(float delay)
    {
        CorrectPanel.SetActive(false);
        WrongPanel.SetActive(true);
        yield return new WaitForSeconds(delay);
        WrongPanel.SetActive(false);
    }
}
