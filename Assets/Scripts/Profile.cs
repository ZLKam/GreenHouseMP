using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Profile : MonoBehaviour
{
    public static string gender;
    public static string username;
    public bool genderFilled;
    public bool usernameFilled;
    public bool ProfileSet;

    public GameObject MenuItems;
    public TMP_InputField inputField;
    public GameObject profilestuff;

    public TextMeshProUGUI textcon;
    public Image ProfileImage;

    public Sprite MaleImage;
    public Sprite FemaleImage;

    // Start is called before the first frame update
    void Start()
    {
        gender = null;
        username = null;
        //FemaleImage = (Sprite)Resources.Load("FemaleWorkerPortrait");
        //MaleImage = (Sprite)Resources.Load("MaleWorkerPortrait");
    }

    // Update is called once per frame
    void Update()
    {
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
        //Debug.Log(genderFilled);
        //Debug.Log(usernameFilled);
        if (ProfileSet)
        {
            //Image sprite = ProfileImage.GetComponent<Image>();
            if (gender == "Male")
            {
                ProfileImage.sprite = MaleImage;
            }
            if (gender == "Female")
            {
                ProfileImage.sprite = FemaleImage;
            }
            textcon.text = username;
        }
    }


    public void GenderSetMale()
    {
        gender = "Male";
    }

    public void GenderSetFemale()
    {
        gender = "Female";
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
