using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtons : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject bgButton;
    public GameObject volumeButton;
    public GameObject sensitivityButton;
    public GameObject resetButton;

    [Header("ON / OFF")]
    public GameObject bgOn;
    public GameObject bgOff;
    public GameObject volOn;
    public GameObject volOff;

    [Header ("Displays")]
    public GameObject bgPanel;
    public GameObject volPanel;
    public GameObject sensPanel;

    // Start is called before the first frame update
    void Start()
    {

        //ClickOnBg();
    }

    //sets the buttons for the settings when in the game to turn the buttons on
    public void ClickOnBg()
    {
        bgButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        volumeButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        sensitivityButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);

        bgPanel.SetActive(true);
        volPanel.SetActive(false);
        sensPanel.SetActive(false);
        resetButton.SetActive(false);
    }

    //sets the panels for the volume settings to active
    public void ClickOnVolume() 
    {
        bgButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        volumeButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        sensitivityButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);

        bgPanel.SetActive(false);
        volPanel.SetActive(true);
        sensPanel.SetActive(false);
        resetButton.SetActive(false);
    }
    

    //sets the panels for the sensitivity settings to active
    public void ClickOnSensitivity() 
    {
        bgButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        volumeButton.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        sensitivityButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        bgPanel.SetActive(false);
        volPanel.SetActive(false);
        sensPanel.SetActive(true);
        resetButton.SetActive(true);
    }

    public void BgOffButton() 
    {
        bgOn.SetActive(true);
        bgOff.SetActive(false);
    }

    public void BgOnButton() 
    {
        bgOn.SetActive(false);
        bgOff.SetActive(true);
    }

    public void VolOffButton()
    {
        volOn.SetActive(true);
        volOff.SetActive(false);
    }

    public void VolOnButton()
    {
        volOn.SetActive(false);
        volOff.SetActive(true);
    }

    public void ShowLogFile()
    {
        try
        {
#if UNITY_STANDALONE
        Application.OpenURL("file://" + Application.persistentDataPath + "/errorLog.txt");
#endif
#if UNITY_ANDROID
            Application.OpenURL("/mnt/sdcard" + "/errorLog.txt");
#endif
        }
        catch (Exception e)
        {
            Debug.Log("Failed to open error log file. " + e.Message);
        }
    }
}
