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
    public bool placementChecks;

    //private Transform[] childrenInModel;
    //private List<Renderer> childRenderList = new List<Renderer>();

    public Shader shaderRight;
    public Shader shaderWrong;

    [SerializeField]
    private Sprite correctSprite;
    [SerializeField]
    private Sprite wrongSprite;

    public void AnswerCheck()
    {
        PlacementCheck();
        if (coolingTower && ahu && chiller && cwp_opt && cwp_opt_Elav && !wrongSelection)
        {
            correctPanel.SetActive(true);
            if (!PlayerPrefs.HasKey(Strings.ChapterTwoLevelOneCompleted))
            {
                if (PlayerPrefs.HasKey(Strings.ChapterTwoProgressions))
                {
                    int progress = PlayerPrefs.GetInt(Strings.ChapterTwoProgressions);
                    progress++;
                    PlayerPrefs.SetInt(Strings.ChapterTwoProgressions, progress);
                }
                else
                {
                    PlayerPrefs.SetInt(Strings.ChapterTwoProgressions, 1);
                }
                PlayerPrefs.SetInt(Strings.ChapterTwoLevelOneCompleted, 1);
            }
        }
        else
        {
            wrongPanel.SetActive(true);
        }
    }

    void PlacementCheck()
    {
        ResetChecks();

        if (correctSelection1[0].transform.childCount > 1 &&
            correctSelection1[0].transform.GetChild(1).CompareTag("Component/CoolingTower"))
        {
            coolingTower = true;
            SetBorderColor(correctSelection1[0].transform.GetChild(0), coolingTower);
        }
        else
        {
            coolingTower = false;
            SetBorderColor(correctSelection1[0].transform.GetChild(0));
        }

        if (correctSelection2.All(i => i.transform.childCount > 1) && 
            correctSelection2.All(i => i.transform.GetChild(1).CompareTag("Component/AHU")))
        {
            ahu = true;
            SetBorderColor(null, false, correctSelection2);
        }
        else
        {
            ahu = false;
            SetBorderColor(null, false, correctSelection2);
        }

        if(correctSelection1[1].transform.childCount > 1 &&
            correctSelection1[1].transform.GetChild(1).CompareTag("Component/Chiller"))
        {
            
            chiller = true;
            SetBorderColor(correctSelection1[1].transform.GetChild(0), chiller);
        }
        else
        {
            chiller = false;
            SetBorderColor(correctSelection1[1].transform.GetChild(0));

        }

        if (correctSelection1[2].transform.childCount > 1 &&
            correctSelection1[2].transform.GetChild(1).CompareTag("Component/CwpOpt") && !cwp_opt)
        {
            cwp_opt = true;
            SetBorderColor(correctSelection1[2].transform.GetChild(0), cwp_opt);
        }
        else if(correctSelection1[2].transform.childCount > 1 &&
            correctSelection1[2].transform.GetChild(1).CompareTag("Component/CwpOptElavated") && !cwp_opt_Elav)
        {
            cwp_opt_Elav = true;
            SetBorderColor(correctSelection1[2].transform.GetChild(0), cwp_opt_Elav);
        }
        else
        {
            cwp_opt_Elav = false;
            cwp_opt = false;
            SetBorderColor(correctSelection1[2].transform.GetChild(0));
        }

        if(correctSelection1[3].transform.childCount > 1 &&
            correctSelection1[3].transform.GetChild(1).CompareTag("Component/CwpOptElavated") && !cwp_opt_Elav)
        {
            cwp_opt_Elav = true;
            SetBorderColor(correctSelection1[3].transform.GetChild(0), cwp_opt_Elav);
        }
        else if (correctSelection1[3].transform.childCount > 1 &&
            correctSelection1[3].transform.GetChild(1).CompareTag("Component/CwpOpt") && !cwp_opt)
        {
            cwp_opt = true;
            SetBorderColor(correctSelection1[3].transform.GetChild(0), cwp_opt);
        }
        else
        {
            cwp_opt = false;
            cwp_opt_Elav = false;
            SetBorderColor(correctSelection1[3].transform.GetChild(0));
        }

        placementChecks = true;

        if (incorrectSelection.Any(i => i.transform.childCount > 0))
        {
            wrongSelection = true;
        }
        else
        {
            wrongSelection = false;
        }
    }

    private void SetBorderColor(Transform spriteToChange = null, bool correct = false, List<GameObject> listToCheck = null)
    {
        if (correct && listToCheck == null)
        {
            spriteToChange.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            spriteToChange.gameObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
        }
        else if(listToCheck == null)
        {
            spriteToChange.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            spriteToChange.gameObject.GetComponent<SpriteRenderer>().sprite = wrongSprite;
        }

        if (listToCheck != null)
        {
            for (int i = 0; i < listToCheck.Count; i++)
            {
                if (listToCheck[i].transform.childCount > 1 && listToCheck[i].transform.GetChild(1).CompareTag("Component/AHU"))
                {
                    listToCheck[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    listToCheck[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
                }
                else 
                {
                    listToCheck[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    listToCheck[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = wrongSprite;
                }
            }
        }
    }


    private void ResetChecks() 
    {
        coolingTower = false;
        ahu = false;
        chiller = false;
        cwp_opt = false;
        cwp_opt_Elav = false;
    }

}
