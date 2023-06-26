using System.Collections;
using System.Collections.Generic;
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

}
