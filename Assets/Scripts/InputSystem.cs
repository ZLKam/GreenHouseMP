using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //SceneManager.LoadScene("Level 2");
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

    //public bool isStationaryTest(int touchCount, float initialDist, float deltaDist = 0) 
    //{
    //    if (initialDist - deltaDist > ) 
    //    {

    //    }
    //}

    public bool isStationary(int touchCount, float initialDist, float deltaDistance = 0f)
    {
        //Debug.Log(Mathf.Abs(initialDist - deltaDistance));
        if (/*(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved) &&*/
                Mathf.Abs(initialDist - deltaDistance) < 1f)
        {
            return true;
        }
        return false;
        //int stationaryTouch = 0;
        //for (int i = 0; i < touchCount; i++)
        //{
        //    if (Input.GetTouch(i).phase == TouchPhase.Stationary)
        //    {
        //        stationaryTouch++;
        //    }
        //}
        //return stationaryTouch == touchCount;
    }

    //returns the right click for the mouse
    public bool RightClick()
    {
#if UNITY_STANDALONE
        return Input.GetMouseButtonDown(2);
#endif
#if UNITY_ANDROID
        return true;
#endif
    }
}

