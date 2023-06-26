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

    public List<Material> skyboxes;

    void Start()
    {
        if (PlayerPrefs.GetInt("firstTime", 1) == 1)
        {
            Debug.Log("First Time Opening");

            FindObjectOfType<AudioManager>().Play("Airport Lounge (Music)");
            //PlayerPrefs.SetString("musicName", "Airport Lounge");

            PlayerPrefs.SetFloat("musicValue", 50);
            PlayerPrefs.SetFloat("soundValue", 50);

            PlayerPrefs.SetFloat("rotationSpeed", 60);
            PlayerPrefs.SetFloat("zoomSpeed", 75);
            PlayerPrefs.SetFloat("zoomSensitivity", 5);

            PlayerPrefs.SetInt("firstTime", 0);
        }
        else
        {
            Debug.Log("NOT First Time Opening");

        }
        
        if (!menu)
        rotationSpeed = PlayerPrefs.GetFloat("rotationSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        CheckSkybox();

        transform.LookAt(focusPoint);
        transform.Translate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    void CheckSkybox()
    {
        RenderSettings.skybox = skyboxes[PlayerPrefs.GetInt("backgroundIndex")];
    }
}
