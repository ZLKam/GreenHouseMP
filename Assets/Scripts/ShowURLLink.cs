using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAndroidOpenUrl;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowURLLink : MonoBehaviour, IPointerClickHandler
{
    private string url;

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
}
