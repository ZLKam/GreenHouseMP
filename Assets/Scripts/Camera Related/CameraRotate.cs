using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CameraRotate : MonoBehaviour
{
    public Transform focusPoint;
    public float rotationSpeed;
    public bool menu;

    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;
    public GameObject characterSelect;

    public List<Material> skyboxes;

    void Start()
    {
        CheckSkybox();

        if (!PlayerPrefs.HasKey("firstTime"))
        {
            Debug.Log("First Time Opening");

            FindObjectOfType<AudioManager>().Play("Airport Lounge (Music)");
            //PlayerPrefs.SetString("musicName", "Airport Lounge");

            PlayerPrefs.SetFloat("musicValue", 50);
            PlayerPrefs.SetFloat("soundValue", 50);

            PlayerPrefs.SetFloat("rotationSpeed", 60);
            PlayerPrefs.SetFloat("zoomSpeed", 75);
            PlayerPrefs.SetFloat("zoomSensitivity", 5);
            PlayerPrefs.SetInt("firstTime", 1);
        }
        else
        {
            Debug.Log("NOT First Time Opening");
            if (FloorSelection.FromPreviewLevel)
            {
                DirectlyShowLevelSelect();
                FloorSelection.FromPreviewLevel = false;
            }
        }
        
        if (!menu)
        rotationSpeed = PlayerPrefs.GetFloat("rotationSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(focusPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void CheckSkybox()
    {
        RenderSettings.skybox = skyboxes[PlayerPrefs.GetInt("backgroundIndex")];
    }

    private void DirectlyShowLevelSelect()
    {
        if (mainMenuPanel.activeSelf)
            mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
        characterSelect.SetActive(false);
    }
}
