using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBg : MonoBehaviour
{
    [SerializeField]
    private RawImage backgroundToScroll;
    [SerializeField]
    private float scrollSpeedX, scrollSpeedY;

    // Update is called once per frame
    void Update()
    {
        Scrolling(backgroundToScroll);
    }

    private void Scrolling(RawImage background) 
    {
        background.uvRect = new Rect(background.uvRect.position + new Vector2(scrollSpeedX, scrollSpeedY) * Time.deltaTime, background.uvRect.size);
    }
}
