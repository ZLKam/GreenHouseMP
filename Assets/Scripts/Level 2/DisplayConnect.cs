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
        if (connection.points.Count > 0)
        {

        }
    }

    public void FromSelected(GameObject component)
    {
        ImgFrom.sprite = component.GetComponent<Sprite>();
    }

    public void ToSelected(GameObject component)
    {
        ImgTo.sprite = component.GetComponent<Sprite>();
    }

    public void PipeClicked(GameObject pipe)
    {
        PipeSelected.sprite = pipe.GetComponent<Sprite>();
    }


}
