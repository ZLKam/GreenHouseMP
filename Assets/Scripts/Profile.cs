using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
   
    public Image alertPopUp;
    private string alertText = "Make sure a <color=red>Character</color> and a <color=red>Username</color> has been inputted.";
    private bool justShowedAlert = false;

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
            textcon.text = username;
        }
        //FemaleImage = (Sprite)Resources.Load("FemaleWorkerPortrait");
        //MaleImage = (Sprite)Resources.Load("MaleWorkerPortrait");
    }

    // Update is called once per frame
    //void Update()
    //{
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
    //}


    public void GenderSetMale()
    {
        gender = "Male";
        genderFilled = true;
        ProfileImage.sprite = MaleImage;
        femaleBtn.GetComponent<Image>().color = Color.white;
        maleBtn.GetComponent<Image>().color = Color.yellow;
    }

    public void GenderSetFemale()
    {
        gender = "Female";
        genderFilled = true;
        ProfileImage.sprite = FemaleImage;
        femaleBtn.GetComponent<Image>().color = Color.yellow;
        maleBtn.GetComponent<Image>().color = Color.white;

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
            ProfileSet = true;
            TransferInfo = ProfileImage;
            textcon.text = username;
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
