using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempCheck : MonoBehaviour
{
    public GameObject AnswerCheck;
    public GameObject AfterLevel;
    public Camera Camera;
    public GameObject[] TempValues;
    public Light Light;
    public GameObject connection;

    private bool showing;

    [Range(0, 30)]
    public int[] Temps;

    // Start is called before the first frame update
    void Start()
    {
        //Camera = null;
        connection = GameObject.Find("Connections");
    }
    
    public void ReviewLevel()
    {
        Light = FindFirstObjectByType<Light>();
        Light.transform.localRotation = Quaternion.identity;
        Camera = Camera.main;
        Camera.transform.position = new Vector3(-390, -10, 0);
        Camera.transform.rotation = Quaternion.Euler(1,90,0);
        Camera.transform.parent.GetComponent<CameraMovement>().enabled = false;
        connection.transform.GetComponent<Connection>().enabled = false;
        Level2AnswerSheet.showPopUp = true;
        AfterLevel.SetActive(true);
        AnswerCheck.SetActive(false);
    }
    
    public void TemperatureCheck()
    {
        if (TempValues.Length == Temps.Length) 
        {
            if (!showing)
            {
                for (int i = 0; i < TempValues.Length; i++)
                {
                    TempValues[i].gameObject.SetActive(true);
                    TextMeshProUGUI texts = TempValues[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                    texts.text = Temps[i].ToString();
                }
                showing = true;
            }
            else
            {
                for (int i = 0; i < TempValues.Length; i++)
                {
                    TempValues[i].gameObject.SetActive(false);
                }
                showing = false;
            }
        }
    }
}
