using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    float width;
    float height;
    public float minSize;
    public float maxSize;
    Rect rect;

    public byte fadeAmount;
    public byte fadeSpeed;
    public Image fadeImage;
    bool fadeIn;
    bool fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        rect = fadeImage.rectTransform.rect;
        width = rect.width;
        height = rect.height;

        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        FadeInOut();
    }

    public void Enter()
    {
        fadeOut = true;
    }

    public void Exit()
    {
        fadeIn = true;
    }

    void FadeInOut()
    {
        if (fadeIn && fadeAmount > 0)
        {
            fadeOut = false;
            fadeAmount -= fadeSpeed;

            if (fadeAmount <= fadeSpeed)
            {
                fadeAmount = 0;
            }

            fadeImage.color = new Color32(255, 255, 255, fadeAmount);

            if (width < maxSize)
            {
                width += 1;
                height += 1;
                fadeImage.rectTransform.sizeDelta = new Vector2(width, height);
            }

            if (fadeAmount == 0)
            {
                fadeIn = false;
            }
        }

        if (fadeOut && fadeAmount < 255)
        {
            fadeIn = false;
            fadeImage.enabled = true;
            fadeAmount += fadeSpeed;

            if (fadeAmount >= 250)
            {
                fadeAmount = 255;
            }

            fadeImage.color = new Color32(255, 255, 255, fadeAmount);

            if (width > minSize)
            {
                width -= 1;
                height -= 1;
                fadeImage.rectTransform.sizeDelta = new Vector2(width, height);
            }

            if (fadeAmount == 255)
            {
                fadeOut = false;
            }
        }
    }
}
