using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class Instructions : MonoBehaviour
{
    public VideoPlayer video;

    public List<VideoClip> clips = new List<VideoClip>();
    public List<string> tutorialText = new List<string>();
    private int index = 0;
    public GameObject continueBtn;
    public GameObject nextBtn;

    [SerializeField]
    private TextMeshProUGUI textArea;

    // Start is called before the first frame update
    void Start()
    {
        continueBtn.SetActive(false);
        video.clip = clips[index];
        textArea.text = tutorialText[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        index++;
        if (index < clips.Count) 
        {
            video.Stop();
            video.clip = clips[index];
            video.Play();
            textArea.text = tutorialText[index];
            if (index == clips.Count - 1)
            {
                nextBtn.SetActive(false);
                continueBtn.SetActive(true);
            }
        }
    }

    public void Previous() 
    {
        index--;
        video.clip = clips[index];
    }
}
