using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Profile : MonoBehaviour
{
    public static string gender;
    public static string username;
    public bool genderFilled;
    public bool usernameFilled;
    public static bool ProfileSet;

    public GameObject MenuItems;
    public TMP_InputField inputField;
    public GameObject profilestuff;

    public TextMeshProUGUI textcon;
    public Image ProfileImage;
    public static Image TransferInfo;

    public Sprite MaleImage;
    public Sprite FemaleImage;

    public GameObject maleBtn;
    public GameObject femaleBtn;

    // Start is called before the first frame update
    void Start()
    {
        if (!ProfileSet)
        {
            username = null;
            gender = null;
        }
        else
        {
            if (gender == "Male")
            {
                ProfileImage.sprite = MaleImage;
            }
            if (gender == "Female")
            {
                ProfileImage.sprite = FemaleImage;
            }
            TransferInfo = ProfileImage;
            profilestuff.SetActive(false);
            MenuItems.SetActive(true);
        }
        //FemaleImage = (Sprite)Resources.Load("FemaleWorkerPortrait");
        //MaleImage = (Sprite)Resources.Load("MaleWorkerPortrait");
    }

    // Update is called once per frame
    void Update()
    {
        TransferInfo = ProfileImage;
        if (gender != null)
        {
            genderFilled = true;
        }
        else { genderFilled = false; }
        if (username != null)
        {
            usernameFilled = true;
        }
        else { usernameFilled = false; } 

        textcon.text = username;
    }


    public void GenderSetMale()
    {
        gender = "Male";
        ProfileImage.sprite = MaleImage;
        femaleBtn.GetComponent<Image>().color = Color.white;
        maleBtn.GetComponent<Image>().color = Color.yellow;
    }

    public void GenderSetFemale()
    {
        gender = "Female";
        ProfileImage.sprite = FemaleImage;
        femaleBtn.GetComponent<Image>().color = Color.yellow;
        maleBtn.GetComponent<Image>().color = Color.white;

    }

    public void StoreName()
    {
        username = inputField.text;
    }


    public void Continue()
    {
        if (genderFilled && usernameFilled)
        {
            //SceneManager.LoadScene("Main Menu");
            profilestuff.SetActive(false);
            MenuItems.SetActive(true);
            ProfileSet = true;
        }
    }

}
