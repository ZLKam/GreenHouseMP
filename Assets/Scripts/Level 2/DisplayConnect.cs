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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
