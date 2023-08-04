using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAndroidOpenUrl;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowURLLink : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private string url;
    private Color initialColor;

    private void Start()
    {
        url = GetComponent<TextMeshProUGUI>().text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
#if UNITY_STANDALONE
        Application.OpenURL(url);
#endif
#if UNITY_ANDROID
       Application.OpenURL(url);
#endif
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        initialColor = GetComponent<TextMeshProUGUI>().color;
        GetComponent<TextMeshProUGUI>().color = Color.red;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = initialColor;
    }
}
