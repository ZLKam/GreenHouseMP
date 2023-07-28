using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System;

public class Speech : MonoBehaviour
{
    public string[] Speechoptions;
    float timer = 5f;
    public GameObject SpeechBubble;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI Speaker;
    public GameObject Profilebutton;

    void Start()
    {
        //if (!(Profile.TransferInfo.sprite == null))
        if (PlayerPrefs.HasKey("ProfileImage"))
        {
            //Profilebutton.GetComponent<Image>().sprite = Profile.TransferInfo.sprite;
            byte[] profileImageData = Convert.FromBase64String(PlayerPrefs.GetString("ProfileImage"));
            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.LoadImage(profileImageData);
            Profilebutton.GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        }
        else
        {
            Profilebutton.GetComponent<Image>().sprite = null;
        }
    }

    public void speech()
    {
        SpeechBubble.SetActive(true);
        speechText.text = Speechoptions[UnityEngine.Random.Range(0, Speechoptions.Length)];
        //Speaker.text = Profile.username;
        Speaker.text = PlayerPrefs.GetString("Username");
        StartCoroutine(Counter());
    }

    IEnumerator Counter()
    {
        yield return new WaitForSeconds(timer);
        SpeechBubble.SetActive(false);
    }
}
