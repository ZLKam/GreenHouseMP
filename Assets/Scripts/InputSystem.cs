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

    //Returns mouse Input left click
    //main purpose is to return the first touch detected and return it to be used
    public bool LeftClick()
    {
#if UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Ended) 
            {
                return false;
            }
        }
        return false;

#endif
    }

    //returns the right click for the mouse
    public bool RightClick()
    {
#if UNITY_STANDALONE
        return Input.GetMouseButtonDown(2);
#endif
        return true;
    }
}

