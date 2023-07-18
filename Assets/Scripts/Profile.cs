using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Profile : MonoBehaviour
{
    public string gender;
    public string username;
    public bool genderFilled;
    public bool usernameFilled;



    // Start is called before the first frame update
    void Start()
    {
        gender = null;
        username = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (gender != null)
        {
            genderFilled = true;
        }
        if (username != null)
        {
            usernameFilled = true;
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

    public void Continue()
    {
        if (genderFilled && usernameFilled)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
