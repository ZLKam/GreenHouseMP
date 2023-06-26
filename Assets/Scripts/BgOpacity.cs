using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgOpacity : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Image bgImage;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        bgImage.GetComponent<Image>().color = new Color32(255,255,255, (byte)slider.value);

    }
}
