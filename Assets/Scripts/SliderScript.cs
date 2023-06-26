using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField]
    private Slider m_Slider;

    [SerializeField]
    public TextMeshProUGUI m_SliderText;

    public string valueName;

    // Start is called before the first frame update
    void Start()
    {
        m_Slider.value = PlayerPrefs.GetFloat(valueName);
    }

    // Update is called once per frame
    void Update()
    {
        SliderUpdate();
    }

    public void SliderUpdate() 
    {
        m_SliderText.text = m_Slider.value.ToString("0");
    }
}
