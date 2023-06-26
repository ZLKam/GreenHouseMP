using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown background;
    public TMP_Dropdown musicDrop;

    public Slider rotate;
    public Slider zoomSpeed;
    public Slider zoomSensitivity;
    public Slider fadeSlider;

    public Slider music;
    public Slider sound;

    private float[] fadeSpeedArray = new[] { 1, 5, 51, 127.5f, 255 };

    // Start is called before the first frame update
    void Start()
    {
        background.value = PlayerPrefs.GetInt("backgroundIndex");
        musicDrop.value = PlayerPrefs.GetInt("musicIndex");
        fadeSlider.value = PlayerPrefs.GetFloat("fadeValue");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusic()
    {
        PlayerPrefs.SetInt("musicIndex", musicDrop.value);
        FindObjectOfType<AudioManager>().Stop(PlayerPrefs.GetString("musicName") + " (Music)");

        for(int i = 0; i < musicDrop.options.Count; i++)
        {
            if(i == PlayerPrefs.GetInt("musicIndex"))
            {
                PlayerPrefs.SetString("musicName", musicDrop.options[i].text);
            }
        }

        Debug.Log(PlayerPrefs.GetString("musicName"));

        FindObjectOfType<AudioManager>().Play(PlayerPrefs.GetString("musicName") + " (Music)");
    }

    public void SetBackground()
    {
        PlayerPrefs.SetInt("backgroundIndex", background.value);
    }

    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("musicValue", music.value);
    }

    public void SetSoundVolume()
    {
        PlayerPrefs.SetFloat("soundValue", sound.value);
    }

    public void SetRotateSpeed()
    {
        PlayerPrefs.SetFloat("rotationSpeed", rotate.value);
    }

    public void SetZoomSpeed()
    {
        PlayerPrefs.SetFloat("zoomSpeed", zoomSpeed.value);
    }

    public void SetZoomSensitivity()
    {
        PlayerPrefs.SetFloat("zoomSensitivity", zoomSensitivity.value);
    }

    public void ResetValues()
    {
        PlayerPrefs.SetFloat("rotationSpeed", 60);
        PlayerPrefs.SetFloat("zoomSpeed", 75);
        PlayerPrefs.SetFloat("zoomSensitivity", 5);
        PlayerPrefs.SetFloat("fadeSpeed", 1);

        rotate.value = PlayerPrefs.GetFloat("rotationSpeed");
        zoomSpeed.value = PlayerPrefs.GetFloat("zoomSpeed");
        zoomSensitivity.value = PlayerPrefs.GetFloat("zoomSensitivity");
        fadeSlider.value = PlayerPrefs.GetFloat("fadeValue");
        fadeSlider.transform.parent.transform.parent.GetChild(fadeSlider.transform.parent.childCount + 1).GetComponent<Fade>().fadeSpeed =
            (byte)PlayerPrefs.GetFloat("fadeSpeed");
    }

    public void SetFadeSpeed()
    {
        fadeSlider.transform.parent.transform.parent.GetChild(fadeSlider.transform.parent.childCount + 1).GetComponent<Fade>().fadeSpeed =
            (byte)fadeSpeedArray[Mathf.RoundToInt(fadeSlider.value)];
        PlayerPrefs.SetFloat("fadeValue", fadeSlider.value);
        PlayerPrefs.SetFloat("fadeSpeed", fadeSpeedArray[Mathf.RoundToInt(fadeSlider.value)]);
    }
}
