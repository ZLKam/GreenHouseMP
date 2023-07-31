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
    public GameObject PreLevelItems;
    public static bool Previewed;

    public UnityEngine.UI.Button btnLevelSelect;

    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject instructionPanel;

    public bool PauseCheck;

    [SerializeField]
    private Image progressionChapter1;
    [SerializeField]
    private Image progressionChapter2;
    [SerializeField]
    private Image progressionChapter3;

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
                    /*Profile.ProfileSet = */Instructions.Read = Previewed = false;
                    PlayerPrefs.DeleteKey("ProfileImage");
                    PlayerPrefs.DeleteKey("Username");
                    PlayerPrefs.DeleteKey("TimeProfileCreated");
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
        FindObjectOfType<AudioManager>()?.Play("Click");

        //specifies the scene to transition to for update
        transitionScene = scene;

        //resets fadeout and read(past tense) for instructions
        fadeOut = true;
        Instructions.Read = false;
    }

    public void TransitionNoSceneChange() 
    //fades in and out but does not change the scene
    {
        FindAnyObjectByType<AudioManager>()?.Play("Click");
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

    public void PreviewTrue()
    {
        Previewed = true;
    }

    public void PlayButton()
    {
        GameMenu.SetActive(false);
        Section.SetActive(true);
        ShowBadges();
    }

    public void SectionSelect()
    {
        //open up the level select popup
        PreLevelItems.SetActive(true);
        Section.SetActive(false);
    }

    public void LevelSelect()
    {
        if (Previewed)
        {
            PreLevelItems.SetActive(false);
            Level.SetActive(true);
        }
    }

    public void SectionBack()
    {
        GameMenu.SetActive(true);
        Section.SetActive(false);
    }

    public void LevelBack()
    {
        Level.SetActive(false);
        PreLevelItems.SetActive(true);
        btnLevelSelect.interactable = true;
    }

    public void PrelevelBack()
    {
        PreLevelItems.SetActive(false);
        Section.SetActive(true);
        ShowBadges();
    }

    private void ShowBadges()
    {
        if (PlayerPrefs.HasKey(Strings.ChapterTwoProgressions))
        {
            int progress = PlayerPrefs.GetInt(Strings.ChapterTwoProgressions);
            progressionChapter2.fillAmount = progress / 3f;
        }
    }
}
