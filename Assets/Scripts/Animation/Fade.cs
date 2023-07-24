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

    public bool PauseCheck;

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
       //Darken();

        //when fading in is true
        // and the fade amount(fade alpha) is more than 0
        if (fadeIn && fadeAmount > 0)
        {
            //disables fading out
            //sets the alpha to the fade amount
            //reduces fade alpha over update
            fadeOut = false;
            fadeImage.color = new Color32(red, green, blue, fadeAmount);
            fadeAmount -= fadeSpeed;

            if (fadeAmount <= 0)
            //once fade alpha is zero
            //disables the fading in and the fade image 
            {
                fadeIn = false;
                fadeImage.enabled = false;
            }
        }

        if (fadeOut && fadeAmount < 255)
        //when fading out of scene
        //and fade amount(fade alpha) is less than 255(max alpha number)
        {
            //disables fading in and renables the fade image
            //sets the fade amount as the alph
            //begins increasing fade alpha
            fadeIn = false;
            fadeImage.enabled = true;
            fadeImage.color = new Color32(red, green, blue, fadeAmount);
            fadeAmount += fadeSpeed;

            if (fadeAmount >= 255)
            //once fade alpha hits the limit
            {
                if (exit)
                //quits the game if true
                {
                    Profile.ProfileSet = Instructions.Read = false;
                    Application.Quit();
                }
                else if (!string.IsNullOrEmpty(transitionScene))
                //loads the next scene if transition scene is specificed, set in editor
                {
                    SceneManager.LoadScene(transitionScene);
                }
                else 
                //just switches between fading in and out
                {
                    fadeOut = false;
                    fadeIn = true;
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

    public void Transition(string scene)
    //takes in a string as the scene destination
    {
        //resets time scale while playing click sound
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>().Play("Click");

        //specifies the scene to transition to for update
        transitionScene = scene;

        //resets fadeout and read(past tense) for instructions
        fadeOut = true;
        Instructions.Read = false;
    }

    public void TransitionNoSceneChange() 
    //fades in and out but does not change the scene
    {
        FindAnyObjectByType<AudioManager>().Play("Click");
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
    //quits the game after click sound
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
            PauseCheck = true;
        }
        else 
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            PauseCheck = false;
            Debug.Log(PauseCheck);
        }
    }

    public void InstructionBtn() 
    {
        instructionPanel.SetActive(true);
    }


    //Section Selector / Level Selector


    public void PlayButton()
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
