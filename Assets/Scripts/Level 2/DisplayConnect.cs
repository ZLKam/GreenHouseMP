using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayConnect : MonoBehaviour
{
    public Connection connection;



    private Transform From = null;
    private Transform To = null;

    public Image ComponentFrom;
    public Image ComponentTo;
    public Image PipeSelected;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPipe(GameObject Pipe)
    {
        PipeSelected.sprite = Pipe.GetComponent<Sprite>();
    }

    public void SelectComponentFrom(GameObject FromComponent)
    {
        ComponentFrom.sprite = FromComponent.GetComponent<Sprite>();
    }

    public void SelectComponentTo(GameObject ToComponent)
    {
        ComponentTo.sprite = ToComponent.GetComponent<Sprite>();
    }
}
