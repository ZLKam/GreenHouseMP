using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownScript : MonoBehaviour
{
    public List<GameObject> backgroundImages;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateImage();
    }

    public void UpdateImage()
    {
        foreach(GameObject background in backgroundImages)
        {
            if (background == backgroundImages[PlayerPrefs.GetInt("backgroundIndex")])
            {
                background.SetActive(true);
            }
            else
            {
                background.SetActive(false);
            }
        }
    }
}
