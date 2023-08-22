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
            if (connection.points.Count == 1)
            {
                var name = connection.points[0].name;
                if (name == "AHU")
                {
                    ImgFrom.sprite = (Sprite)Resources.Load("GameUI/Icons/Level2/AHULVL2");
                }
                else if (name == "Cooling Tower")
                {

                }
            }
            else if (connection.points.Count == 2)
            {

            }
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
