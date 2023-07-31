using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Text;
using System;

public class Profile : MonoBehaviour
{
    //public static string gender;
    //public static string username;
    private string username;
    public bool genderFilled;
    public bool usernameFilled;
    //public static bool ProfileSet;

    public GameObject MenuItems;
    public TMP_InputField inputField;
    public GameObject profilestuff;

    public TextMeshProUGUI textcon;
    public Image ProfileImage;
    //public static Image TransferInfo;

    public Sprite MaleImage;
    public Sprite FemaleImage;

    public GameObject maleBtn;
    public GameObject femaleBtn;
   
    public Image alertPopUp;
    private string alertText = "Make sure a <color=red>Character</color> and a <color=red>Username</color> has been inputted.";
    private bool justShowedAlert = false;

    private byte[] selectedProfileImage;
    private string timeProfileCreated;

    // Start is called before the first frame update
    void Start()
    {
        //if (!ProfileSet)
        //{
        //    username = null;
        //    gender = null;
        //}
        //else
        //{
        //    if (gender == "Male")
        //    {
        //        ProfileImage.sprite = MaleImage;
        //    }
        //    if (gender == "Female")
        //    {
        //        ProfileImage.sprite = FemaleImage;
        //    }
        //    TransferInfo = ProfileImage;
        //    profilestuff.SetActive(false);
        //    MenuItems.SetActive(true);
        //    textcon.text = username;
        //}
        if (PlayerPrefs.HasKey("TimeProfileCreated"))
        {
            DateTime dateTime = DateTime.Now;
            DateTime profileCreatedTime = DateTime.Parse(PlayerPrefs.GetString("TimeProfileCreated"));
            double minutesSinceProfileCreated = (dateTime - profileCreatedTime).TotalMinutes;
            if (minutesSinceProfileCreated > 10)
            {
                PlayerPrefs.DeleteKey("ProfileImage");
                PlayerPrefs.DeleteKey("Username");
                PlayerPrefs.DeleteKey("TimeProfileCreated");
                PlayerPrefs.DeleteKey("LevelProgressions");
            }
        }
        if (PlayerPrefs.HasKey("Username") && PlayerPrefs.HasKey("ProfileImage"))
        {
            Texture2D texture2D = new(1, 1);
            texture2D.LoadImage(Convert.FromBase64String(PlayerPrefs.GetString("ProfileImage")));
            ProfileImage.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
            profilestuff.SetActive(false);
            MenuItems.SetActive(true);
            textcon.text = PlayerPrefs.HasKey("Username") ? PlayerPrefs.GetString("Username") : string.Empty;
        }
        //FemaleImage = (Sprite)Resources.Load("FemaleWorkerPortrait");
        //MaleImage = (Sprite)Resources.Load("MaleWorkerPortrait");
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Continue();
        }
        //    TransferInfo = ProfileImage;
        //    if (gender != null)
        //    {
        //        genderFilled = true;
        //    }
        //    else { genderFilled = false; }
        //    if (username != null)
        //    {
        //        usernameFilled = true;
        //    }
        //    else { usernameFilled = false; } 

        //    textcon.text = username;
    }


    public void GenderSetMale()
    {
        //gender = "Male";
        genderFilled = true;
        ProfileImage.sprite = MaleImage;
        femaleBtn.GetComponent<Image>().color = Color.white;
        maleBtn.GetComponent<Image>().color = Color.yellow;
        selectedProfileImage = ImageConversion.EncodeToPNG(MaleImage.texture);
        PlayerPrefs.SetString("ProfileImage", Convert.ToBase64String(selectedProfileImage));
    }

    public void GenderSetFemale()
    {
        //gender = "Female";
        genderFilled = true;
        ProfileImage.sprite = FemaleImage;
        femaleBtn.GetComponent<Image>().color = Color.yellow;
        maleBtn.GetComponent<Image>().color = Color.white;
        selectedProfileImage = ImageConversion.EncodeToPNG(FemaleImage.texture);
        PlayerPrefs.SetString("ProfileImage", Convert.ToBase64String(selectedProfileImage));
    }

    public void StoreName()
    {
        if (inputField.text == string.Empty)
        {
            usernameFilled = false;
        }
        else
        {
            username = inputField.text;
            usernameFilled = true;
        }
    }


    public void Continue()
    {
        if (justShowedAlert)
            return;
        if (genderFilled && usernameFilled)
        {
            //SceneManager.LoadScene("Main Menu");
            profilestuff.SetActive(false);
            MenuItems.SetActive(true);
            //ProfileSet = true;
            //TransferInfo = ProfileImage;
            textcon.text = username;
            PlayerPrefs.SetString("Username", username);
            timeProfileCreated = DateTime.Now.ToString();
            PlayerPrefs.SetString("TimeProfileCreated", timeProfileCreated);
        }
        else if (!genderFilled || !usernameFilled)
        {
            Debug.Log("Show alert");
            alertPopUp.gameObject.SetActive(true);
            alertPopUp.GetComponentInChildren<TextMeshProUGUI>().text = alertText;
            StartCoroutine(CloseAlert());
        }
    }

    private IEnumerator CloseAlert()
    {
        yield return new WaitForNextFrameUnit();
        float time = 0f;
        while (true)
        {
            if (justShowedAlert)
            {
                time += Time.deltaTime;
                if (time >= 0.5f)
                {
                    justShowedAlert = false;
                    yield break;
                }
                yield return null;
            }
            time += Time.deltaTime;
            if (InputSystem.Instance.LeftClick())
            {
                alertPopUp.gameObject.SetActive(false);
                time = 0f;
                justShowedAlert = true;
            }
            if (time >= 2f)
            {
                alertPopUp.gameObject.SetActive(false);
                time = 0f;
                justShowedAlert = true;
            }
            yield return null;
        }
    }

}
