using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempCheck : MonoBehaviour
{
    public GameObject AnswerCheck;
    public GameObject AfterLevel;
    public Camera Camera;
    public TextMeshProUGUI[] TempValues;

    [Range(0, 30)]
    public int[] Temps;

    // Start is called before the first frame update
    void Start()
    {
        Camera = null;
    }
    
    public void ReviewLevel()
    {
        //Camera = Camera.main;
        //Camera.transform.position = (Location for more 2D view)
        Level2AnswerSheet.showPopUp = true;
        AfterLevel.SetActive(true);
        AnswerCheck.SetActive(false);
    }
    
    public void TemperatureCheck()
    {
        //if (TempValues.Length == Temps.Length) { }
        //for (int i = 0; i < TempValues.Length; i++)
        //{
        //    TempValues[i].text = Temps[i].ToString();
        //}

    }
}
