using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempCheck : MonoBehaviour
{
    public GameObject AnswerCheck;
    public GameObject Proceed;
    public GameObject Temperature;
    public Camera Camera;
    public TextMeshProUGUI[] TempValues;

    [Range(0, 30)]
    public int[] Temps;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void ReviewLevel()
    {
        //Camera = Camera.main;
        //Camera.transform.position = (Location for more 2D view)
        Level2AnswerSheet.showPopUp = true;
        AnswerCheck.SetActive(false);
        Proceed.SetActive(true);
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
