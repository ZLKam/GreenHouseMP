using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = text2.text;
    }
}
