using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Mouse Left Click
    /// </summary>
    /// <returns></returns>
    public bool LeftClick()
    {
#if UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
#endif

#if UNITY_ANDROID
        return Input.GetTouch(0);
#endif
    }

    public bool RightCLick()
    {
#if UNITY_STANDALONE
        return Input.GetMouseButtonDown(2);
#endif

#if Unity_ANDROID
        if (Input.touchCount > 0)
        {
            return true;
        }
        return false;
#endif
    }
}

