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

    // Start is called before the first frame update
    void Start()
    {
        Profilebutton.GetComponent<Image>().sprite = Profile.TransferInfo.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
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
