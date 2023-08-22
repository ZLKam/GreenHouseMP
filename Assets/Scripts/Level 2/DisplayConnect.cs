using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayConnect : MonoBehaviour
{
    public Connection connection;


    public Image ImgFrom;
    public Image ImgTo;
    public Image PipeSelected;

    public Sprite transparentSprite;

    private void Awake()
    {
        ImgFrom.sprite = transparentSprite;
        ImgTo.sprite = transparentSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (connection.points.Count == 0)
        {
            Reset();
        }
    }

    public void FromSelected(Image component)
    {
        ImgFrom.sprite = component.sprite;
    }

    public void ToSelected(Image component)
    {
        ImgTo.sprite = component.sprite;
    }

    public void PipeClicked(Image pipe)
    {
        PipeSelected.sprite = pipe.sprite;
    }

    public void Reset()
    {
        PipeSelected.sprite = ImgFrom.sprite = ImgTo.sprite = transparentSprite;
    }
}
