using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public bool exit;
    public bool settings;

    public byte maxFade;
    public byte fadeAmount;
    public byte fadeSpeed;
    public byte red, green, blue;
    Image fadeImage;
    public bool fadeIn;
    public bool fadeOut;

    public bool canDarken;
    [HideInInspector]
    public bool darken;

    public GameObject proceedButton;

    public string transitionScene;
    public string previousScene;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("fadeSpeed"))
        {
            PlayerPrefs.SetFloat("fadeSpeed", 51);
        }
        fadeSpeed = (byte)PlayerPrefs.GetFloat("fadeSpeed");
        Debug.Log(PlayerPrefs.GetString("previousScene"));
        previousScene = PlayerPrefs.GetString("previousScene");

        fadeImage = GetComponent<Image>();
        fadeImage.color = new Color32(0, 0, 0, fadeAmount);
    }

    // Update is called once per frame
    void Update()
    {
        Darken();

        if (fadeIn && fadeAmount > 0)
        {
            fadeOut = false;
            fadeImage.color = new Color32(red, green, blue, fadeAmount);
            fadeAmount -= fadeSpeed;

            if(fadeAmount <= 0)
            {
                fadeIn = false;
                fadeImage.enabled = false;
            }
        }

        if (fadeOut && fadeAmount < 255)
        {
            fadeIn = false;
            fadeImage.enabled = true;
            fadeImage.color = new Color32(red, green, blue, fadeAmount);
            fadeAmount += fadeSpeed;

            if(fadeAmount >= 255)
            {
                Debug.Log("transition");

                if (exit)
                {
                    Debug.Log("Quit");
                    Application.Quit();
                }
                else
                {
                    SceneManager.LoadScene(transitionScene);
                }
            }
        }
    }

    void Darken()
    {
        if (canDarken)
        {
            if (darken)
            {
                if (fadeAmount < maxFade)
                {
                    fadeImage.enabled = true;
                    fadeImage.color = new Color32(red, green, blue, fadeAmount);
                    fadeAmount += fadeSpeed;
                }
            }
            else
            {
                if (fadeAmount > 0)
                {
                    fadeImage.color = new Color32(red, green, blue, fadeAmount);
                    fadeAmount -= fadeSpeed;

                    if (fadeAmount <= 0)
                    {
                        if(proceedButton != null)
                        {
                            proceedButton.SetActive(true);
                        }
                        fadeImage.enabled = false;
                    }
                }
            }
        }
    }

    public void transition(string scene)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        transitionScene = scene;

        fadeOut = true;
    }

    public void PreviousScene()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        Debug.Log(PlayerPrefs.GetString("previousScene"));

        transitionScene = previousScene;

        fadeOut = true;
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        fadeOut = true;
        exit = true;
    }
}
