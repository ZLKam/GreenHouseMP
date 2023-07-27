using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        if (Profile.TransferInfo.sprite == null)
        {
            Profilebutton.GetComponent<Image>().sprite = null;
        }
        else
        {
            Profilebutton.GetComponent<Image>().sprite = Profile.TransferInfo.sprite;
        }
    }

    public void speech()
    {
        SpeechBubble.SetActive(true);
        speechText.text = Speechoptions[Random.Range(0, Speechoptions.Length)];
        Speaker.text = Profile.username;
        StartCoroutine(Counter());
    }

    IEnumerator Counter()
    {
        yield return new WaitForSeconds(timer);
        SpeechBubble.SetActive(false);
    }
}
