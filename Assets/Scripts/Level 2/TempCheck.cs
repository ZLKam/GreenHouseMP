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
    public Light Light;

    private bool showing;

    [Range(0, 30)]
    public int[] Temps;

    // Start is called before the first frame update
    void Start()
    {
        Camera = null;
    }
    
    public void ReviewLevel()
    {
        Light = FindFirstObjectByType<Light>();
        Light.transform.localRotation = Quaternion.identity;
        Camera = Camera.main;
        Camera.transform.position = new Vector3(-120, -3, 0);
        Camera.transform.rotation = Quaternion.Euler(0,90,0);
        Camera.transform.parent.GetComponent<CameraMovement>().enabled = false;
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
                    TempValues[i].text = Temps[i].ToString();
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
