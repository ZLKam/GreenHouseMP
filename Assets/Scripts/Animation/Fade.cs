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

    public GameObject Section;
    public GameObject Level;
    public GameObject GameMenu;

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject instructionPanel;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("fadeSpeed"))
        {
            PlayerPrefs.SetFloat("fadeSpeed", 51);
        }
        fadeSpeed = (byte)PlayerPrefs.GetFloat("fadeSpeed");
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

            if (fadeAmount <= 0)
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
            //fadeAmount = (byte)Mathf.Lerp(0, 255, fadeSpeed * Time.deltaTime);

            if (fadeAmount >= 255)
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
                        if (proceedButton != null)
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
        Time.timeScale = 1;
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

    //Pause Menus
    public void PauseGame() 
    {
        if (!pausePanel.activeInHierarchy)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void InstructionBtn() 
    {
        instructionPanel.SetActive(true);
    }


    //Section Selector / Level Selector


    public void playbtn()
    {
        GameMenu.SetActive(false);
        Section.SetActive(true);
    }

    public void SectionSelect()
    {
        //open up the level select popup
        Level.SetActive(true);
        Section.SetActive(false);
    }

    public void SectionBack()
    {
        GameMenu.SetActive(true);
        Section.SetActive(false);
    }

    public void LevelBack()
    {
        Level.SetActive(false);
        Section.SetActive(true);
    }

}
